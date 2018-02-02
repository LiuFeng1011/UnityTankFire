using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹数据
/// </summary>
public class BulletData  {
	public int 		weaponid	{get; private set;}//武器
	public int 		force		{get; private set;}//攻击力
	public float 	distance	{get; private set;}//距离
	public float 	speed		{get; private set;}//速度
	public int 		source		{get; private set;}//发出子弹的玩家

	public Vector3 	startpos	{get; private set;}//起始位置
	public Vector3 	forward		{get; private set;}//方向

	public BulletData(int weaponid,int force,float dis,float speed,int source,Vector3 startpos,Vector3 forward){
		this.weaponid 	= weaponid;
		this.force 		= force;
		this.distance	= dis;
		this.speed 		= speed;
		this.source		= source;
		this.startpos 	= startpos;
		this.forward 	= forward;
	}
}
