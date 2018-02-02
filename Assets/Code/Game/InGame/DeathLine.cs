using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		//进入触发器执行的代码
	}
	void OnCollisionEnter(Collision collision) 
	{
		//进入碰撞器执行的代码
		InGameRole role = collision.transform.GetComponent<InGameRole>();
		if(role == null) {
			return;
		}
		//杀死对象

		InGameManager.instance.playerManager.RoleLife(role.data.id,-99999999,true);
	}
}
