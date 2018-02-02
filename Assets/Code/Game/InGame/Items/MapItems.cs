using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItems : InGameBaseObject {

	public enum MapItemType{
		none,
		scores,
		health,
		magnet,
		bomb,
		weapon
	}

	public items_list conf{get;private set;}

	public static MapItems CreateItem(int itemid){
		items_list _conf = ConfigManager.itemListManager.GetData(itemid);
		GameObject itemGo = (GameObject)MonoBehaviour.Instantiate(ResManager.GetPrefabsRes(_conf.items_model));
		MapItems item = itemGo.AddComponent<MapItems>();
		item.Init(_conf);
		return item;
 	}

	public void Init(items_list conf){
		this.conf = conf;
	}

	public override ObjType GetType ()
	{
		return ObjType.item;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 碰撞逻辑
	/// </summary>
	/// <param name="role">Role.</param>
	void Hit(InGameRole role){
		switch((MapItemType)conf.items_type){
		case MapItemType.scores:
			InGameManager.instance.playerManager.AddScores(role.data.id,conf.items_parameter);
			break;
		case MapItemType.health:
			//特效
			InGameManager.instance.gameEffectManager.AddEffect(60010040,role.gameObject,Vector3.zero);
			InGameManager.instance.playerManager.RoleLife(role.data.id,conf.items_parameter);
			break;
		case MapItemType.magnet:
			BaseBuff buf = new BaseBuff(role,BaseBuff.BuffType.absorb,conf.items_parameter,InGameManager.gameTime,10);
			InGameManager.instance.playerManager.AddBuf(role.data.id,buf);
			break;
		case MapItemType.bomb:
			InGameManager.instance.playerManager.RoleLife(role.data.id,-conf.items_parameter);
			break;
		case MapItemType.weapon:
			InGameManager.instance.playerManager.ChangeWeapon(role.data.id,conf.items_parameter);
			break;
		}
	}

	/// <summary>
	/// 碰撞
	/// </summary>
	/// <param name="collider">Collider.</param>
	void OnTriggerEnter(Collider collider)
	{
		//进入触发器执行的代码
//		Debug.Log("MapItems OnTriggerEnter");
		InGameRole role = collider.transform.GetComponent<InGameRole>();
		if(role == null) {
			return;
		}

		Hit(role);
		InGameManager.instance.inGameItemManager.DelItem(this);
		Destroy(gameObject);
	}
}
