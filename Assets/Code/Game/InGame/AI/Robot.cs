using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// author:liufeng 
/// date:170809
/// 机器人类
/// </summary>
public class Robot : MonoBehaviour{
	
	public InGameRole role{get;private set;}

	List<BaseAIBehavior> behaviors = new List<BaseAIBehavior>();

	//当前执行的行为
	BaseAIBehavior runingBehavior = null;

	// Use this for initialization
	void Start () {
		role = transform.GetComponent<InGameRole>();

		behaviors.Add((new AIBehaviorAtk()).Init(this));
		behaviors.Add((new AIBehaviorFlee()).Init(this));
		behaviors.Add((new AIBehaviorGetItem()).Init(this));
	}
	
	// Update is called once per frame
	void Update () {
		BaseAIBehavior bestBehavior = null;
		for(int i = 0 ; i < behaviors.Count ; i ++){
			behaviors[i].BehaviorUpdate();
			if(bestBehavior == null || bestBehavior.weight < behaviors[i].weight){
				bestBehavior = behaviors[i];
			}
		}
		runingBehavior = bestBehavior;

		if(runingBehavior != null){
			runingBehavior.Run();
		}
	}
}
