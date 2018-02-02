using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏场景物体基础类
/// </summary>
public abstract class InGameBaseObject : MonoBehaviour {

	public enum ObjType{
		role,
		item,
		bullet,
	}

	public abstract ObjType GetType();
}
