using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHeavy : BaseWeapon {

	public override bool Fire (InGameRole role,Vector3 pos, Vector3 direction)
	{

		if(!base.Fire(role,pos,direction)){
			return false;
		}

		//创建子弹
		BulletData bd = new BulletData(data.weapons_id,data.weapons_atk,data.weapons_range,data.weapons_speed,role.data.id,
			role.GetFirePoint(),role.transform.forward);
		InGameManager.instance.bulletManager.AddBullet(bd);
		return true;
	}
}
