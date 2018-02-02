using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffect : MonoBehaviour {
	public conf_effect conf{get;private set;}
	ParticleSystem ps ;
	long playTime;

	public void Init(conf_effect conf){
		this.conf = conf;
		ps = transform.GetComponent<ParticleSystem>();
		gameObject.SetActive(false);
	}

	public void Play(){
		playTime = InGameManager.gameTime;

		gameObject.SetActive(true);

		ps.Play();

	}

	void Update(){

		if(conf.loop == 1){
			return;
		}

		if(InGameManager.gameTime - playTime > ps.main.duration*1000){
			Die();
		}
	}

	public void Die(){
		gameObject.transform.parent = null;
		gameObject.SetActive(false);

		//Destroy(gameObject);
	}

	void OnDestroy(){
		Debug.Log("Destroy effect !!!!!!!!!!!!!!! : " + conf.id);
	}
}
