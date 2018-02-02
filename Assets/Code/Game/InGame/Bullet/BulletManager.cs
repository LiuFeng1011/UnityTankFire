using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹管理器
/// </summary>
public class BulletManager {
	List<Bullet> bullets = new List<Bullet>();
	// Update is called once per frame
	public void Update () {
		for(int i = 0 ; i < bullets.Count ; i ++){
			if(bullets[i] == null) continue;
			bullets[i].BulletUpdate();
		}
	}

	public void AddBullet(BulletData data){
		Bullet bullet = Bullet.CreateBullet(data);
		bullets.Add(bullet);
	}

	public void RemoveBullet(Bullet bullet ){
		bullets.Remove(bullet);
	}
}
