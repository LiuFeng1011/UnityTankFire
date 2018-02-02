using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// author:liufeng
/// date:170807
/// 游戏摄像机管理管理器
/// </summary>
public class InGameCamera : MonoBehaviour {
	Vector3 basePosition ;
	InGameRole followTarget = null;

	public void SetTarget(InGameRole target){
		followTarget = target;
	}
	void Awake(){
		if(!ConfigManager.loadDown) return;
		basePosition = new Vector3(0,ConfigManager.normalLevelManager.data.main_camera_high,ConfigManager.normalLevelManager.data.main_camera_pos);
		transform.eulerAngles = new Vector3(ConfigManager.normalLevelManager.data.main_camera_rotate,0,0);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(followTarget == null) return;
		if(followTarget.IsDie()) return;
		Vector3 targetpos = new Vector3(followTarget.transform.position.x,
			basePosition.y,
			followTarget.transform.position.z - basePosition.z);
		Vector3 addition = (targetpos - transform.position) * 0.5f;
		transform.position += addition;
	}
}
