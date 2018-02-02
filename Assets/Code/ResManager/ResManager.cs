using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager {

	public static Object GetPrefabsRes(string name){
		return Resources.Load("Prefabs/"+name);
	}

	public static Object GetRes(string path){
		return Resources.Load(path);
	}
}
