using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gametest : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Vector2 from_ = new Vector2(1f,1f);
		Vector2 to_ = new Vector2(0,0);

		Debug.Log(Vector2.Angle(from_,to_)) ;
//		Vector2 v3 = Vector2.Cross(from_,to_);  
//		Debug.Log(v3);
//		if(v3.z > 0)  
//			  
//		else  
//			Debug.Log(360-Vector2.Angle(from_,to_));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
