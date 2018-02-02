using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng 
/// date:170807
/// des:游戏logo展示场景管理，也可进行一些初始化工作
/// </summary>
public class GameLogoManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Debug.Log("=========GameLogoManager===========");
		Invoke("NextScene",1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void NextScene(){
		ChangeScene.ExchangeScene(ChangeScene.SceneTag.Update);
	}

}
