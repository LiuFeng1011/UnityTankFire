using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹
/// </summary>
public class Bullet : InGameBaseObject {
	BulletData data;
	GameEffect effect;
	public static Bullet CreateBullet(BulletData data){
		weapons_list w = ConfigManager.weaponListManager.GetData(data.weaponid);
		if(w == null) return null;

		GameObject bGo = (GameObject)MonoBehaviour.Instantiate(ResManager.GetPrefabsRes(w.weapons_model));  
		Bullet b = bGo.AddComponent<Bullet>();
		b.Init(data);
		return b;
	}

	public override ObjType GetType ()
	{
		return ObjType.bullet;
	}


	private void Init(BulletData data){
		this.data = data;

		this.transform.position = data.startpos;
		this.transform.forward = data.forward;

		int effectid = ConfigManager.weaponListManager.GetData(data.weaponid).bullet_tail_effect;
		effect = InGameManager.instance.gameEffectManager.AddEffect(effectid,gameObject,Vector3.zero);
	}

	public void BulletUpdate(){

		this.transform.Translate(Vector3.forward*Time.deltaTime*data.speed);

		if(Vector3.Distance(transform.position,data.startpos) > data.distance){
			Die();
		}
	}

	public void Die(){
		InGameManager.instance.gameEffectManager.AddWorldEffect(ConfigManager.weaponListManager.GetData(data.weaponid).bullet_bomb_effect,transform.position);

		if(effect != null){
			effect.Die();
		}

		InGameManager.instance.bulletManager.RemoveBullet(this);
		Destroy(gameObject);
	}

	bool Hit(InGameRole role){
//		if(role.data.id == this.data.source){
//			return false;
//		}
//
		if(role.IsReturnBullet()){
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+180,transform.eulerAngles.z);
			return false;
		}

		InGameManager.instance.playerManager.RoleLife(role.data.id,-data.force);
		return true;
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

		if(!Hit(role)){
			return;
		}
		Die();
	}
}
