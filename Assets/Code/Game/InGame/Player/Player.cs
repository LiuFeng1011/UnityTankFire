using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	public InGameRole role{get;private set;}

	int weaponid ;
	public Player(InGameRole role){
		this.role = role;
		weaponid = role.weapon.data.weapons_id;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		if(!InGameManager.instance.inGameUIManager.joyStickControl.IsPress()){
			return ;
		}

		Vector3 v = InGameManager.instance.inGameUIManager.joyStickControl.GetVector();
		float rotation = GameCommon.GetVectorAngle(new Vector3(1,0,0),v);

		InGameManager.instance.playerManager.RoleRotation(role.data.id,360-rotation + 90);

		if(weaponid != role.weapon.data.weapons_id){
			weaponid = role.weapon.data.weapons_id;
			InGameManager.instance.inGameUIManager.ChangePlayerWeapon(weaponid);
		}
	}

	public void Fire(){
		InGameManager.instance.playerManager.RoleFire(role.data.id);
	}
}
