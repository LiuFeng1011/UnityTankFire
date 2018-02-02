using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIPlayerInfoManager {
	Transform playerInfo;

	Dictionary<int,InGameUIPlayerInfoUnit> infoList = new Dictionary<int,InGameUIPlayerInfoUnit>();

	public void Init(Transform playerInfo){
		this.playerInfo = playerInfo;

	}

	//Update
	public void InGameUIPlayerInfoManagerUpdate(){
//		Dictionary<int,InGameRole> playerList = InGameManager.instance.playerManager.GetPlayerList();
		//寻找玩家
		foreach (KeyValuePair<int,InGameUIPlayerInfoUnit> kv in infoList)
		{
			if(!GameCommon.IsPositionInScreen(kv.Value.role.transform.position)){
				kv.Value.gameObject.SetActive(false);
				continue;
			}
			kv.Value.gameObject.SetActive(true);
			kv.Value.InGameUIPlayerInfoUnitUpdate();
		}
	}

	public void AddRole(InGameRole role){
		if(infoList.ContainsKey(role.data.id)){
			return;
		}
		CreateInfo(role);
	}

	void CreateInfo(InGameRole role){

		GameObject roleGo = NGUITools.AddChild(playerInfo.gameObject,(GameObject)ResManager.GetPrefabsRes("PlayerInfoUnit"),1);

		InGameUIPlayerInfoUnit unit = roleGo.GetComponent<InGameUIPlayerInfoUnit>();
		unit.Init(role);
		infoList.Add(role.data.id,unit);
	}
}
