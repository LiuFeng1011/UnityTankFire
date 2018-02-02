using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill {
	InGameRole role;
	skill_list conf;

	long fireTime;

	public BaseSkill(InGameRole role, int skillId){
		fireTime = 0;
		conf = ConfigManager.skillListManager.GetSkill(skillId);
		this.role = role;
	}

	public void Fire(){
		if(InGameManager.gameTime - fireTime < conf.skill_cd*1000){
			return;
		}
		fireTime = InGameManager.gameTime;

		switch(conf.skill_id){
		case 4001001:
			//加速技能参数是几倍
			BaseBuff buf = new BaseBuff(role,BaseBuff.BuffType.moveSpeed, 1,InGameManager.gameTime,conf.skill_time);
			InGameManager.instance.playerManager.AddBuf(role.data.id, buf);

			break;

		case 4001002:
			BaseBuff defbuf = new BaseBuff(role,BaseBuff.BuffType.noEnemy, 1,InGameManager.gameTime,conf.skill_time);
			InGameManager.instance.playerManager.AddBuf(role.data.id, defbuf);

			//反弹子弹
			BaseBuff returnbuf = new BaseBuff(role,BaseBuff.BuffType.returnBullet, 1,InGameManager.gameTime,1);
			InGameManager.instance.playerManager.AddBuf(role.data.id, returnbuf);

			break;
			default:
			break;
		}
	}
}
