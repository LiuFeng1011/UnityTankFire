using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏内玩家数据
/// </summary>
public class InGameRoleData {
	public int 		id;
	public string 	name{get;private set;}
	public int 		level{get;private set;}
	public int 		head{get;private set;}
	public int		tankid{get;private set;}

	public InGameRoleData(string name,int level,int head,int tankid){
		this.name = name;
		this.level = level;
		this.head = head;
		this.tankid = tankid;
	}
}
