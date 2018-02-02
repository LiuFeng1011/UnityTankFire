using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏buff
/// author:liufeng 
/// date:170809
/// </summary>
public class BaseBuff {
	public enum BuffType{
		moveSpeed,	//移动速度
		absorb,		//吸收距离
		rotateSpeed,//旋转速度
		cantMove,	//无法移动
		noEnemy,	//无敌
		returnBullet,//反弹子弹
	}

	public BuffType type		{get;private set;}
	public float 	param		{get;private set;}//数值，不同buff不同意义
	public long 	createTime	{get;private set;}//开始时间
	public int 		holdTime	{get;private set;}//持续时间

	InGameRole role;
	GameEffect effect;
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseBuff"/> class.
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="param">数值，不同buff不同意义.</param>
	/// <param name="createTime">开始时间.</param>
	/// <param name="holdTime">持续时间.</param>
	public BaseBuff(InGameRole role, BuffType type,float param,long createTime,float holdTime){
		
		Init(role,type,param,createTime,holdTime);
	}

	/// <summary>
	/// 获取值
	/// </summary>
	/// <returns>The parameter.</returns>
	/// <param name="t">T.</param>
	public float GetParam(BuffType t){
		if(type != t || !IsValid()){
			return 0;
		}
		return param;
	}

	public bool IsValid(){
		return InGameManager.gameTime - createTime < holdTime;
	}

	public virtual void Init(InGameRole role, BuffType type,float param,long createTime,float holdTime){
		this.role = role;
		this.type = type;
		this.param = param;
		this.createTime = createTime;
		this.holdTime = (int)(holdTime*1000f);

		switch(type){
		case BuffType.moveSpeed:
			effect = InGameManager.instance.gameEffectManager.AddEffect(60010050,role.gameObject,Vector3.zero);
			break;
		case BuffType.noEnemy:
			effect = InGameManager.instance.gameEffectManager.AddEffect(60010060,role.gameObject,Vector3.zero);
			break;
		case BuffType.absorb:
			effect = InGameManager.instance.gameEffectManager.AddEffect(60010080,role.gameObject,Vector3.zero);
			break;
		}
	}

	public void Over(){
		if(effect != null)
			effect.Die();
	}

}
