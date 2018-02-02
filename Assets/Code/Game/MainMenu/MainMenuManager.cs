using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("===========MainMenuManager===============");
		GameObject playbutton = GameObject.Find("UI Root/PlayBtn");
		UIEventListener.Get(playbutton).onClick = PlayCB; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayCB(GameObject obj){
		ChangeScene.ExchangeScene(ChangeScene.SceneTag.InGame);
	}
}
