using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 武器基础类
/// </summary>
public abstract class BaseWeapon  {

	long fireTime = 0;

	public weapons_list data;
	int firecount;
	/// <summary>
	/// 工厂
	/// </summary>
	/// <returns>The weapon.</returns>
	/// <param name="type">Type.</param>
	public static BaseWeapon CreateWeapon(int weaponid){
		BaseWeapon ret = null;
		switch(weaponid){
		case 3001001 : 
			ret = new WeaponNormal();
			break;
		case 3001002 : 
			ret = new WeaponHeavy();
			break;
		case 3001003 : 
			ret = new WeaponDisperse();
			break;
		default:
			ret = new WeaponNormal();
			break;
		}
		ret.Init(weaponid);

		return ret;
	}

	public void Init(int id){
		data = ConfigManager.weaponListManager.GetData(id);
		firecount = (int)data.weapons_cost;
	}

	public virtual bool Fire(InGameRole role,Vector3 pos,Vector3 direction){
		if(!IsCanFire()){
			return false;
		}
		firecount --;
		fireTime = InGameManager.gameTime;

		GameEffect eff = InGameManager.instance.gameEffectManager.AddEffect(60010030,role.gameObject,Vector3.zero /*role.GetFireLocalPoint()*/);
		if(eff != null) eff.transform.forward = direction;
		return true;
	}

	//是否无效
	public bool IsUseless(){
		return firecount <= 0;
	}

	/// <summary>
	/// 是否可以开火
	/// </summary>
	/// <returns><c>true</c> if this instance is can fire; otherwise, <c>false</c>.</returns>
	protected bool IsCanFire(){
		if(InGameManager.gameTime - fireTime < data.weapons_cd * 1000){
			return false;
		}
		return true;
	}

}
