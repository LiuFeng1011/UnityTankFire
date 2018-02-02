using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIRankManager  {

	float lastFlushTime = 0;
	float flustTime = 1;

	Transform rankList;
	InGameUIRankUnit selfrank;
	UIGrid grid;

	List<InGameUIRankUnit> list = new List<InGameUIRankUnit>();

	public InGameUIRankManager(Transform rankList){
		this.rankList = rankList;
		grid = rankList.Find("Scroll View").Find("Grid").GetComponent<UIGrid>();

		selfrank = rankList.Find("self").GetComponent<InGameUIRankUnit>();
	}

	// Update is called once per frame
	public void InGameUIRankManagerUpdate () {
		lastFlushTime += Time.deltaTime;
		if(lastFlushTime < flustTime){
			return;
		}
		lastFlushTime = 0;

		Dictionary<int,InGameRole> roleMap = InGameManager.instance.playerManager.GetPlayerList();

		List<InGameRole> rankList = new List<InGameRole>();

		//把全部玩家填入临时数组
		foreach (KeyValuePair<int,InGameRole> kv in roleMap)
		{
			rankList.Add(kv.Value);
		}

		//排序
		rankList.Sort(delegate(InGameRole x, InGameRole y)
		{
				return y.scores.CompareTo(x.scores);
		});

		int myRank = -1;

		//设置值
		for(int i = 0 ; i < rankList.Count ; i ++){
			InGameUIRankUnit unit;
			if(list.Count <= i){
				unit = CreateUnit();
			}else{
				unit = list[i];
			}

			Debug.Log(i + "  list.Count: " + list.Count + "  rankList.Count: " + rankList.Count);
			unit.SetVal(i +  1,rankList[i]);
			unit.gameObject.SetActive(true);

			if(rankList[i].data.id == InGameManager.instance.player.role.data.id){
				myRank = -1;
			}
		}
		//删除多余对象
		for(int i = rankList.Count ; i < list.Count ; i ++){
			list[i].gameObject.SetActive(false);
		}

		//设置自己的排名
		if(myRank != -1){
			selfrank.gameObject.SetActive(true);
			selfrank.SetVal(myRank + 1,rankList[myRank]);
		}else{
			selfrank.gameObject.SetActive(false);
		}

	}

	InGameUIRankUnit CreateUnit(){

		GameObject rankgo = NGUITools.AddChild(grid.gameObject,(GameObject)ResManager.GetPrefabsRes("PlayerRankUnit"),1);

		InGameUIRankUnit unit = rankgo.GetComponent<InGameUIRankUnit>();
		list.Add(unit);

		grid.Reposition();
		return unit;
	}
}
