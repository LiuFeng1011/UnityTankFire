using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
/// <summary>
/// 吃道具AI
/// </summary>
public class AIBehaviorGetItem : BaseAIBehavior {
	public const float startDis = 15;

	float[] rotateWeight = {0,0,0,0};//四个方向的权重 
	// Use this for initialization
	void Start () {
		
	}
	

	public override void BehaviorUpdate(){
		base.BehaviorUpdate();

		Vector2 basePoint = InGameManager.instance.mapManager.basePoint;
		Vector2 mapSize = InGameManager.instance.mapManager.mapSize;

		int rotatedirection = -1;

		//检测边界
		if(robot.role.transform.forward.x < 0 && robot.role.transform.position.x < basePoint.x + startDis){
			rotatedirection = 0;
		}else if(robot.role.transform.forward.x > 0 && robot.role.transform.position.x > basePoint.x + mapSize.x - startDis){
			rotatedirection = 1;
		}else if(robot.role.transform.forward.z < 0 && robot.role.transform.position.z < basePoint.y + startDis){
			rotatedirection = 2;
		}else if(robot.role.transform.forward.z > 0 && robot.role.transform.position.z > basePoint.y + mapSize.y - startDis){
			rotatedirection = 3;
		}
		if(rotatedirection != -1 ){
			float addition = robot.role.GetSpeed()*Time.deltaTime + Random.Range(0,Time.deltaTime);
			this.weight += addition;
			rotateWeight[rotatedirection] += addition;
		} else{
			this.weight = 10;
			rotateWeight[(int)Random.Range(0,4)] += (robot.role.GetSpeed()*Time.deltaTime / 10) + Random.Range(0,Time.deltaTime);
		}

	}


	public override void Run(){
		Vector2 basePoint = InGameManager.instance.mapManager.basePoint;
		Vector2 mapSize = InGameManager.instance.mapManager.mapSize;

		float targetRotate = 0;
		//检测边界
		int rotatedirection = -1;

		for(int i = 0 ; i < 4 ; i ++){
			if(rotatedirection == -1 && rotateWeight[i] > 1){
				rotatedirection = i;
			}else if(rotatedirection != -1 && rotateWeight[i] > rotateWeight[rotatedirection]){
				rotatedirection = i;
			}
		}

		if(rotatedirection >= 0){
			switch(rotatedirection){
			case 0 :
				InGameManager.instance.playerManager.RoleRotation(robot.role.data.id,Random.Range(30,150));
				break;
			case 1 :
				InGameManager.instance.playerManager.RoleRotation(robot.role.data.id,Random.Range(210,330));
				break;
			case 2 :
				InGameManager.instance.playerManager.RoleRotation(robot.role.data.id,Random.Range(-60,60));
				break;
			case 3 :
				InGameManager.instance.playerManager.RoleRotation(robot.role.data.id,Random.Range(120,240));
				break;
			default:
				//Do nothing
				break;

			}

			for(int i = 0 ; i < 4 ; i ++){
				rotateWeight[i] = 0;
			}
		}


		//InGameManager.instance.playerManager.RoleRotation(robot.role.data.id,360-rotation + 90);
	}
}
