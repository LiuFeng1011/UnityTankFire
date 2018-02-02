using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏管理器
/// </summary>
public class InGameManager : MonoBehaviour {

	public MapManager 			mapManager			{get;private set;}
	public InGameItemManager 	inGameItemManager	{get;private set;}
	public InGameUIManager 		inGameUIManager		{get;private set;}
	public PlayerManager 		playerManager		{get;private set;}
	public BulletManager 		bulletManager		{get;private set;}
	public GameEffectManager 	gameEffectManager	{get;private set;}

	TimeMonitor timeMonitor;

	public static InGameManager instance {get;private set;}
	public static long gameTime;

	public Player player{get;private set;}

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		timeMonitor = new TimeMonitor(5);
		//地图管理器需要优先初始化
		mapManager 			= new MapManager();
		inGameItemManager 	= gameObject.AddComponent<InGameItemManager>();
		inGameUIManager 	= new InGameUIManager();
		playerManager		= new PlayerManager();
		bulletManager		= new BulletManager();
		gameEffectManager	= new GameEffectManager();
		inGameUIManager.Init();

		CreateRold();

		//初始化相机
		InGameCamera camera = GameObject.Find("Main Camera").AddComponent<InGameCamera>();
		camera.SetTarget(player.role);
	}
	
	// Update is called once per frame
	void Update () {
		gameTime = GameCommon.GetTimeStamp(false);

		timeMonitor.Start();

		mapManager.Update();
		timeMonitor.Step("mapManager");

		inGameItemManager.ItemManagerUpdate();

		timeMonitor.Step("inGameItemManager");

		inGameUIManager.Update();
		timeMonitor.Step("inGameUIManager");

		playerManager.Update();
		timeMonitor.Step("playerManager");

		player.Update();
		timeMonitor.Step("player");

		bulletManager.Update();
		timeMonitor.Step("bulletManager");

		timeMonitor.Stop();
	}

	/// <summary>
	/// 创建玩家
	/// </summary>
	void CreateRold(){
		InGameRoleData playerdata = new InGameRoleData("aa",1,1,2001001);
		InGameRole role = playerManager.AddRole(playerdata);

		player = new Player(role);

		//机器人
		for(int i = 0 ; i < ConfigManager.normalLevelManager.data.Normal_level_ai ; i ++){
			InGameRoleData robotdata = new InGameRoleData("robot_"+i,1,1,2001001);
			InGameRole robot = playerManager.AddRole(robotdata);

			robot.transform.position = InGameManager.instance.mapManager.GetRandPoint();
			robot.Rotate(UnityEngine.Random.Range(0,360));

			robot.gameObject.AddComponent<Robot>();


			GameObject model = robot.transform.Find("tank").Find("tank").gameObject;
			model.GetComponent<SkinnedMeshRenderer>().materials[0].mainTexture = GameCommon.GetResource("Model/png/tank_1") as Texture;

		}
	}


	void Destory(){
		instance = null;
	}
}
