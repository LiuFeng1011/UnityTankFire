using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// bate:170809
/// 机器人行为基础类
/// </summary>
public abstract class BaseAIBehavior {

	protected Robot robot;

	public float weight{get;protected set;}//权重

	public BaseAIBehavior Init(Robot r){
		weight = 0;
		this.robot = r;
		return this;
	}

	public virtual void BehaviorUpdate(){
		
	}

	public virtual void Run(){
		
	}
}
