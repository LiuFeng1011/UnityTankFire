using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170812
/// 游戏内特效管理器
/// </summary>
public class GameEffectManager {

	//特效池
	Dictionary<int ,List<GameEffect>> effectPool = new Dictionary<int ,List<GameEffect>>();

	public GameEffectManager(){

		//创建特效,加入到池里
		List<conf_effect> _conf = ConfigManager.confEffectManager.datas;

		for(int i = 0 ; i < _conf.Count ; i ++){
			conf_effect conf = _conf[i];
			for(int j = 0 ; j < conf.repeat_count ; j++){
				GameEffect eff = CreateEffect(conf.id);
				if(eff != null){
					if(!effectPool.ContainsKey(conf.id)){
						effectPool.Add(conf.id,new List<GameEffect>());
					}
					effectPool[conf.id].Add(eff);

				}
			}
		}

	}


	/// <summary>
	/// 添加特效到世界
	/// </summary>
	/// <returns>The world effect.</returns>
	/// <param name="effectid">Effectid.</param>
	/// <param name="pos">Position.</param>
	public GameEffect AddWorldEffect(int effectid,Vector3 pos){
		GameEffect ge = GetEffect(effectid,pos);
		if(ge == null) return null;
		ge.transform.position = pos;
		ge.Play();
		return ge;
	}

	/// <summary>
	/// 添加特效到服务器
	/// </summary>
	/// <returns>The world effect.</returns>
	/// <param name="effectid">Effectid.</param>
	/// <param name="pos">Position.</param>
	public GameEffect AddEffect(int effectid,GameObject obj,Vector3 pos){
		GameEffect ge = GetEffect(effectid,obj.transform.position + pos);
		if(ge == null) return null;
		ge.transform.parent = obj.transform;
		ge.transform.localPosition = pos;
		ge.transform.forward = obj.transform.forward;
		ge.Play();
		return ge;
	}

	GameEffect GetEffect(int effectid ,Vector3 worldPos){
		//对应池是否存在
		if(!effectPool.ContainsKey(effectid)){
			return null;
		}


		//寻找空闲特效
		List<GameEffect> pool = effectPool[effectid];
		GameEffect ret = null;
		for(int i = 0 ; i < pool.Count ; i ++){
			GameEffect eff = pool[i];

			if(eff.gameObject.activeSelf){
				continue;
			}

			ret = eff;
			break;
		}

		if(ret != null && ret.conf.out_show == 0){
			//不在屏幕内
			if(!GameCommon.IsPositionInScreen(worldPos)){
				return null;
			}
		}

		return ret;
	}

	GameEffect CreateEffect(int effectid){
		conf_effect _conf = ConfigManager.confEffectManager.GetData(effectid);
		Object obj = ResManager.GetRes(_conf.file_path);
		if(obj == null){
			Debug.LogError("cant find file ! : " + _conf.file_path);
		}
		if(_conf.res_type == 1){
			GameObject go = (GameObject)MonoBehaviour.Instantiate(obj);  

			GameEffect ge = go.AddComponent<GameEffect>();
			ge.Init(_conf);

			return ge;
		}
		return null;
	}

}
