using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffManager {

	List<BaseBuff> buffs = new List<BaseBuff>();

	//Update 清理buff
	public void PlayerBuffManagerUpdate(){

		List<BaseBuff> delbuffs = new List<BaseBuff>();
		
		for(int i = 0 ; i < buffs.Count ; i ++){
			if(!buffs[i].IsValid()){
				delbuffs.Add(buffs[i]);
			}
		}

		for(int i = 0 ; i < delbuffs.Count ; i ++){
			RemoveBuf(delbuffs[i]);
		}
	}
		

	/// <summary>
	/// 增加一个buf
	/// </summary>
	/// <param name="buf">Buffer.</param>
	public void AddBuff(BaseBuff buf){
		for(int i = 0 ; i < buffs.Count ; i ++){
			if(buffs[i].type == buf.type){
				RemoveBuf(buffs[i]);
				break;
			}
		}
		buffs.Add(buf);
	}

	/// <summary>
	/// 获取指定类型的buff加成
	/// </summary>
	/// <param name="type">Type.</param>
	public float GetBufValByType(BaseBuff.BuffType type){
		float ret = 0;
		for(int i = 0 ; i < buffs.Count ; i ++){
			ret += buffs[i].GetParam(type);
		}
		return ret;
	}

	void RemoveBuf(BaseBuff buf){
		buf.Over();
		buffs.Remove(buf);
	}

	public void ClearBuff(){
		for(int i = 0 ; i < buffs.Count ; i ++){
			buffs[i].Over();
		}
		buffs.Clear();
	}
}
