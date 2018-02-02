using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItemPositionManager {
	Dictionary<int,Dictionary<int,List<MapItems>>> itemsPositionMap = new Dictionary<int,Dictionary<int,List<MapItems>>>();

	public static int unitSize = 1;

	public InGameItemPositionManager(int unitSize){
		InGameItemPositionManager.unitSize = unitSize;
	}

	public void RemoveItemByPointMap(MapItems item){
		Vector3 pos = item.transform.position;
		int row = GetPosRow(pos);
		int col = GetPosCol(pos);
		itemsPositionMap[row][col].Remove(item);
	}
	public void AddItemToPointMap(MapItems item,Vector3 pos){

		int row = GetPosRow(pos);
		int col = GetPosCol(pos);
		if(!itemsPositionMap.ContainsKey(row)){
			itemsPositionMap.Add(row,new Dictionary<int,List<MapItems>>());
		}
		if(!itemsPositionMap[row].ContainsKey(col)){
			itemsPositionMap[row].Add(col,new List<MapItems>());
		}
		itemsPositionMap[row][col].Add(item);
	}

	/// <summary>
	/// 道具位置改变
	/// </summary>
	/// <param name="item">Item.</param>
	/// <param name="oldPos">Old position.</param>
	/// <param name="newPos">New position.</param>
	public void ItemMove(MapItems item,Vector3 oldPos,Vector3 newPos){

		int oldrow = GetPosRow(oldPos);
		int oldcol = GetPosCol(oldPos);
		int newrow = GetPosRow(newPos);
		int newcol = GetPosCol(newPos);

		if(oldrow != newrow || oldcol != newcol){
			itemsPositionMap[oldrow][oldcol].Remove(item);
			AddItemToPointMap(item,newPos);
		}
	}

	/// <summary>
	/// 获取某个位置的道具列表
	/// </summary>
	/// <returns>The item list by position.</returns>
	/// <param name="pos">Position.</param>
	public List<MapItems> GetItemListByPos(Vector3 pos){
		int row = GetPosRow(pos);
		int col = GetPosCol(pos);
		if(!itemsPositionMap.ContainsKey(row)){
			return null;
		}
		if(!itemsPositionMap[row].ContainsKey(col)){
			return null;
		}
		return itemsPositionMap[row][col];
	}

	public int GetPosRow(Vector3 pos){
		return (int)pos.x/unitSize;
	}
	public int GetPosCol(Vector3 pos){
		return (int)pos.z/unitSize;
	}
}
