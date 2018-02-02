using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏内UI管理器
/// </summary>
public class InGameUIManager{

	public JoyStickControl joyStickControl{get;private set;}

	GameObject 	pad;
	UISprite 	fireBtnIcon;
	UILabel 	scoresLabel;

	InGameUIPlayerInfoManager playerInfo;
	InGameUIRankManager rankManager;
	public void Init(){
		//=================PAD===============
		//JoyStickControl.instance.SetPosition(new Vector3(0f,0f,0f));
		joyStickControl = JoyStickControl.instance;

		Transform pad = GameObject.Find("UI Root").transform.Find("Camera").Find("Pad");
		this.pad = pad.gameObject;

		//开火
		GameObject fireBtn = pad.Find("FireBtn").gameObject;
		UIEventListener.Get(fireBtn).onClick = FireCB; 
		fireBtnIcon = fireBtn.transform.Find("Sprite").GetComponent<UISprite>();

		//加速
		GameObject speedBtn = pad.Find("SpeedBtn").gameObject;
		UIEventListener.Get(speedBtn).onClick = SpeedCB; 

		//防御
		GameObject defenseBtn = pad.Find("DefenseBtn").gameObject;
		UIEventListener.Get(defenseBtn).onClick = DefenseCB; 

		//=================UI===============
		Transform UI = GameObject.Find("UI Root").transform.Find("Camera").Find("UI");
		scoresLabel = UI.Find("scores").GetComponent<UILabel>();


		//玩家信息UI管理
		Transform PlayerInfo = UI.Find("PlayerInfo");

		playerInfo = new InGameUIPlayerInfoManager();
		playerInfo.Init(PlayerInfo);


		Transform rankList = UI.Find("RankList");
		rankManager = new InGameUIRankManager(rankList);
	}

	public void Update () {
		//更新分数
		scoresLabel.text = "scores : " + InGameManager.instance.player.role.scores;

		//更新玩家信息
		playerInfo.InGameUIPlayerInfoManagerUpdate();

		//更新排行信息
		rankManager.InGameUIRankManagerUpdate();
	}

	//增加了一个玩家
	public void AddRole(InGameRole role){
		playerInfo.AddRole(role);
	}

	void FireCB(GameObject obj){
		InGameManager.instance.player.Fire();
	}

	void SpeedCB(GameObject obj){
		InGameManager.instance.playerManager.RoleSkill(InGameManager.instance.player.role.data.id,4001001);
	}

	void DefenseCB(GameObject obj){
		InGameManager.instance.playerManager.RoleSkill(InGameManager.instance.player.role.data.id,4001002);
		
	}

	public void ChangePlayerWeapon(int weaponid){
		switch(weaponid){
		case 3001001:
			fireBtnIcon.spriteName = "combat_fire_1";
			break;
		case 3001002:
			fireBtnIcon.spriteName = "combat_fire_2";
			break;
		case 3001003:
			fireBtnIcon.spriteName = "combat_fire_3";
			break;

		}
	}
}
