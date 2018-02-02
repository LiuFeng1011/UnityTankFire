using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏地图管理器 管理场景中所有物体
/// </summary>
public class MapManager {
	public Vector2 mapSize {get;private set;}
	public Vector2 basePoint {get;private set;}

	public MapManager(){
//		BoxCollider b = GameObject.Find("ground").GetComponent<BoxCollider>();
//		mapSize = new Vector2(b.size.x,b.size.z);
//		basePoint = new Vector2(b.center.x - b.size.x / 2,b.center.z - b.size.z / 2);

		mapSize = new Vector2(100,100);
		basePoint = new Vector2(-50,-50);
		Debug.Log(mapSize +"///"+ basePoint);
	} 

	public Vector3 GetRandPoint(){
		return new Vector3(
			Random.Range(basePoint.x, basePoint.x + mapSize.x),
			0,
			Random.Range(basePoint.y, basePoint.y + mapSize.y));
	}

	public void Update(){

	}

}
