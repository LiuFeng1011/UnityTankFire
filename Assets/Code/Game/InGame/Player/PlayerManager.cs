using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 玩家管理器
/// </summary>
public class PlayerManager {

	Dictionary<int,InGameRole> roleMap = new Dictionary<int,InGameRole>();

	public int autoRoleid = 0;

	public void Update () {
		foreach (KeyValuePair<int,InGameRole> kv in roleMap)
		{
			kv.Value.RoleUpdate();
		}
	}

	/// <summary>
	/// 添加一个角色
	/// </summary>
	public InGameRole AddRole(InGameRoleData roledata){
		autoRoleid ++ ;

		roledata.id = autoRoleid;

		tank_list conf = ConfigManager.tankListManager.GetDataById(roledata.tankid);

		GameObject roleGo = (GameObject)MonoBehaviour.Instantiate(ResManager.GetPrefabsRes(conf.tank_model));  
		InGameRole role = roleGo.AddComponent<InGameRole>();
		role.Init(roledata);

		roleMap.Add(roledata.id,role);

		InGameManager.instance.inGameUIManager.AddRole(role);

		return role;
	}

	public InGameRole GetRole(int roleid){
		if(!roleMap.ContainsKey(roleid)) return null;
		return roleMap[roleid];
	}

	public Dictionary<int,InGameRole> GetPlayerList(){
		return roleMap;
	}

	/// <summary>
	/// 增加分数
	/// </summary>
	public void AddScores(int roleid,int scores){
		InGameRole role = GetRole(roleid);
		if(role == null )return;
		role.AddScores(scores);
	}

	/// <summary>
	/// 删除一个角色
	/// </summary>
	public void DelRole(int roleid){

	}
		
	/// <summary>
	/// 旋转
	/// </summary>
	/// <param name="roleid">Roleid.</param>
	/// <param name="r">The red component.</param>
	public void RoleRotation(int roleid,float r){
		InGameRole role = GetRole(roleid);
		if(role == null )return;
		role.Rotate(r);
	}

	/// <summary>
	/// 开火
	/// </summary>
	/// <param name="roleid">Roleid.</param>
	public void RoleFire(int roleid){
		InGameRole role = GetRole(roleid);
		if(role == null )return;
		role.Fire();
	}

	/// <summary>
	/// 释放技能
	/// </summary>
	/// <param name="roleid">Roleid.</param>
	/// <param name="skillid">Skillid.</param>
	public void RoleSkill(int roleid,int skillid){
		InGameRole role = GetRole(roleid);
		if(role == null )return;
		role.FireSkill(skillid);
	}

	/// <summary>
	/// 角色血量改变
	/// </summary>
	/// <param name="roleid">Roleid.</param>
	/// <param name="life">增减量.</param>
	public void RoleLife(int roleid,int life,bool isforce = false){
		InGameRole role = GetRole(roleid);
		if(role == null ){
			Debug.Log(" cant get role : " + roleid);
			return;
		}
		role.ChangeLife(life,isforce);
	}

	/// <summary>
	/// 切换武器
	/// </summary>
	public void ChangeWeapon(int roleid,int weaponid){
		InGameRole role = GetRole(roleid);
		if(role == null ){
			Debug.Log(" cant get role : " + roleid);
			return;
		}
		role.ChangeWeapon(weaponid);
	}


	public void HitPlayer(int roleid1,int roleid2){
		InGameRole role1 = GetRole(roleid1);
		if(role1 == null ){
			Debug.Log(" cant get role : " + roleid1);
			return;
		}
		InGameRole role2 = GetRole(roleid2);
		if(role2 == null ){
			Debug.Log(" cant get role : " + roleid2);
			return;
		}
		role1.HitPlayer(role2);
	}


	public void AddBuf(int roleid,BaseBuff buf){
		InGameRole role = GetRole(roleid);
		if(role == null ){
			Debug.Log(" cant get role : " + roleid);
			return;
		}
		role.AddBuff(buf);
	}
}
