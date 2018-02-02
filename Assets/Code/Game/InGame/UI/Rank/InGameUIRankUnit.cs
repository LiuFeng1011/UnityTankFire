using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIRankUnit : MonoBehaviour {

	UILabel ranknameLabel ;
	UILabel scoresLabel ;
	void Awake(){
		ranknameLabel = transform.Find("rankname").GetComponent<UILabel>();
		scoresLabel = transform.Find("scores").GetComponent<UILabel>();
	}

	// Use this for initialization
	void Start () {
		
	}

	public void SetVal(int rank,InGameRole role){
		ranknameLabel.text = rank + "." + role.data.name;
		scoresLabel.text = role.scores.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
