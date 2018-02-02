using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbItem  {
	InGameRole role;

	public AbsorbItem(InGameRole role){
		this.role = role;
	}

	public void AbsorbItemUpdate(){
		Draw(role.GetAbsorbDistance());
	}


	public void Draw(float dis){
		if(dis < 1){
			Debug.Log("absorb distance too short!!! dis : " + dis);
			return ;
		}
		float radius = dis;

		float startX = role.transform.position.x - radius;
		float startY = role.transform.position.z - radius;

		float endX = role.transform.position.x + radius;
		float endY = role.transform.position.z + radius;

		float x = startX;
		float y = startY;
		while(x < endX){
			while(y < endY){
				Vector3 targetpos = new Vector3(x,0,y);

				Draw(InGameManager.instance.inGameItemManager.GetItemListByPos(targetpos),dis);

				y += InGameItemPositionManager.unitSize;
			}
			y = startY;
			x += InGameItemPositionManager.unitSize;
		}
	}

	public void Draw( List<MapItems> items,float dis){
		if(items == null ) return;

		List<MapItems> moveitems = new List<MapItems>();
		for(int i = 0 ; i < items.Count ; i ++){
			MapItems item = items[i];
			if(item == null){
				continue;
			}

			//只吸收积分道具
			if(item.conf.items_type != 1){
				continue;
			}

			if(Vector3.Distance(item.transform.position,role.transform.position) < dis){
				moveitems.Add(item);
			}
		}

		for(int i = 0 ; i < moveitems.Count ; i ++){
			MapItems item = moveitems[i];
			Vector3 cutPos = role.transform.position - item.transform.position;

//			float cutDis = Vector3.Distance(new Vector3(0,0,0),cutPos);
//			if(cutDis < 0.01f){
//				continue;
//			}

//			Vector3 additionPos = cutPos.normalized * Time.deltaTime * (ConfigManager.normalLevelManager.data.absorb_speed/cutDis);
			Vector3 additionPos = cutPos.normalized * ConfigManager.normalLevelManager.data.absorb_speed * Time.deltaTime * 0.1f;
			float targetdis = Vector3.Distance(additionPos,Vector3.zero) ;
			float baseDis = Vector3.Distance(cutPos,Vector3.zero);
//
//			if(baseDis <= 0.1f){
//				continue;
//			}
//			Debug.Log(additionPos + " // " + cutPos + " // " + targetdis + " /// " + baseDis);
			if(targetdis > baseDis && targetdis > 0) {
				additionPos = additionPos * (baseDis / targetdis);
			}

			InGameManager.instance.inGameItemManager.ItemMove(item,additionPos);
		}
	}
}
