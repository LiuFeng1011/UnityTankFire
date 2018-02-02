using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏道具管理器
/// </summary>
public class InGameItemManager : MonoBehaviour{
	Dictionary<int,List<MapItems>> itemsMap = new Dictionary<int,List<MapItems>>();
	Dictionary<int,long> itemTimeList = new Dictionary<int,long>();

	InGameItemPositionManager inGameItemPositionManager;

	void Awake(){
		InitData();
	}

	void Start(){
		StartCoroutine(CreateItemTearth()); 
	} 

	/// <summary>
	/// 初始化数组数据
	/// </summary>
	void InitData(){
		inGameItemPositionManager = new InGameItemPositionManager(1);
		List<items_list> itemlist = ConfigManager.itemListManager.datas;

		for(int i = 0 ; i < itemlist.Count ; i ++){
			items_list item = itemlist[i];
			itemsMap.Add(item.items_id,new List<MapItems>());
			itemTimeList.Add(item.items_id,0);
		}
	}
	/// <summary>
	/// 创建道具
	/// </summary>
	/// <returns>The item.</returns>
	/// <param name="itemid">Itemid.</param>
	/// <param name="pos">Position.</param>
	public MapItems AddItem(int itemid,Vector3 pos){
		MapItems ret = MapItems.CreateItem(itemid);
		ret.transform.position = pos;

		itemTimeList[itemid] = InGameManager.gameTime;
		itemsMap[itemid].Add(ret);
		inGameItemPositionManager.AddItemToPointMap(ret,pos);
		return ret;
	}

	/// <summary>
	/// 删除物体
	/// </summary>
	/// <param name="item">Item.</param>
	public void DelItem(MapItems item){
		inGameItemPositionManager.RemoveItemByPointMap(item);
		if(!itemsMap.ContainsKey(item.conf.items_id)){
			return;
		}
		itemsMap[item.conf.items_id].Remove(item);
	}

	/// <summary>
	/// 道具位置改变
	/// </summary>
	/// <param name="item">Item.</param>
	/// <param name="oldPos">Old position.</param>
	/// <param name="newPos">New position.</param>
	public void ItemMove(MapItems item,Vector3 additionPos){
		Vector3 oldPos = item.transform.position;
		item.transform.position += additionPos;
		inGameItemPositionManager.ItemMove(item,oldPos,item.transform.position);
	}

	/// <summary>
	/// 获取某个位置的道具列表
	/// </summary>
	/// <returns>The item list by position.</returns>
	/// <param name="pos">Position.</param>
	public List<MapItems> GetItemListByPos(Vector3 pos){
		return inGameItemPositionManager.GetItemListByPos(pos);
	}

	public void ItemManagerUpdate(){
		
	}

	IEnumerator CreateItemTearth(){
		while(true){
			List<items_list> itemlist = ConfigManager.itemListManager.datas;

			for(int i = 0 ; i < itemlist.Count ; i ++){
				items_list itemconf = itemlist[i];

				//是否满足添加时间
				if(InGameManager.gameTime - itemTimeList[itemconf.items_id] < itemconf.items_time ){
					continue;
				}

				//数量是否满足
				if(itemsMap[itemconf.items_id].Count >= itemconf.items_max){
					continue;
				}

				//添加道具
				AddItem(itemconf.items_id,InGameManager.instance.mapManager.GetRandPoint());

			}
			yield return 0;  
		}
	}


}
