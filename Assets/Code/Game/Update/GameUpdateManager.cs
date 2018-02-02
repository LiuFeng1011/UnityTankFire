using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class GameUpdateManager : MonoBehaviour {
	bool isLoadFinished = false;
	// Use this for initialization
	void Start () {

		Debug.Log("=========GameUpdateManager===========");
		ConfigUpdate.Instance.CheckConfig(FinishedUpdateConf);
	}

	void FinishedUpdateConf(){
		Debug.Log("--------FinishedUpdateConf----------");
		ConfigManager.LoadData();
		NextScene();
//		Thread athread = new Thread(new ThreadStart(LoadData));
//		athread.IsBackground = true;
//		athread.Start();
		//NextScene();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void NextScene(){
		ChangeScene.ExchangeScene(ChangeScene.SceneTag.MainMenu);
	}

}
