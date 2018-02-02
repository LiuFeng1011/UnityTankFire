using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// des:游戏入口，进行一些基础的游戏初始化工作
/// </summary>
public class GateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("=========GateManager===========");
		Invoke("NextScene",0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextScene(){
		ChangeScene.ExchangeScene(ChangeScene.SceneTag.Logo);
	}
}
