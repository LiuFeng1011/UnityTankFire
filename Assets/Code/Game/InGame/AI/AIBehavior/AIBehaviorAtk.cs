using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击AI
/// </summary>
public class AIBehaviorAtk : BaseAIBehavior {

	// Use this for initialization
	void Start () {
		
	}
	
	public override void BehaviorUpdate(){
		base.BehaviorUpdate();

		//附近是否有敌人
		foreach (KeyValuePair<int,InGameRole> kv in InGameManager.instance.playerManager.GetPlayerList())
		{
			if(kv.Value.data.id == robot.role.data.id) continue;
			float dis  = Vector3.Distance(robot.role.transform.position,kv.Value.transform.position);
			if( dis < ConfigManager.normalLevelManager.data.ai_view){
				this.weight += ((robot.role.life - kv.Value.life) / robot.role.maxlife) * Time.deltaTime * 100 + Random.Range(10,50) * Time.deltaTime ;


				if(((float)( kv.Value.life - robot.role.life) / (float)robot.role.maxlife) > 0.1f){
					this.weight = 0;
					break;
				}
			}
		}
	}


	public override void Run(){
		InGameRole target = null;
		//寻找血最少的敌人
		foreach (KeyValuePair<int,InGameRole> kv in InGameManager.instance.playerManager.GetPlayerList())
		{
			if(kv.Value.data.id == robot.role.data.id) continue;
			if(Vector3.Distance(robot.role.transform.position,kv.Value.transform.position) < ConfigManager.normalLevelManager.data.ai_view){
				if(target == null || target.life >  kv.Value.life){
					target = kv.Value;
				}
			}
		}

		if(target == null){
			this.weight = 0;
			return;
		}

		Vector3 v = robot.transform.position - target.transform.position;
		Vector3 _v = new Vector3(v.x,v.z,0);
		float rotation = GameCommon.GetVectorAngle(new Vector3(1,0,0),_v.normalized);
		InGameManager.instance.playerManager.RoleRotation(robot.role.data.id,360 - rotation - 90);

		InGameManager.instance.playerManager.RoleFire(robot.role.data.id);
	}
}
