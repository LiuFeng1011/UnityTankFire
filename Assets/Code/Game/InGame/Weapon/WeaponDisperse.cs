using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisperse : BaseWeapon {

	public override bool Fire (InGameRole role,Vector3 pos, Vector3 direction)
	{
		if(!base.Fire(role,pos,direction)){
			return false;
		}

		float baseRotation = -(ConfigManager.normalLevelManager.data.disperse_bullet_count / 2 * 
								ConfigManager.normalLevelManager.data.disperse_bullet_angle);

		for(int i = 0 ; i < ConfigManager.normalLevelManager.data.disperse_bullet_count ; i ++){
			//创建子弹
			BulletData bd = new BulletData(data.weapons_id,data.weapons_atk,data.weapons_range,data.weapons_speed,role.data.id,
				role.GetFirePoint(),Quaternion.Euler(0,baseRotation,0) * role.transform.forward);
			InGameManager.instance.bulletManager.AddBullet(bd);
			baseRotation += ConfigManager.normalLevelManager.data.disperse_bullet_angle;
		}
		return true;
	}
}
