using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIPlayerInfoUnit : MonoBehaviour {

	public InGameRole role{get ; private set;}

	public UILabel	levelLabel{get ; private set;}
	public UILabel	nameLabel{get ; private set;}
	public UISprite	lifeSprite{get ; private set;}

	Vector3 basePos;
	void Awake(){
		levelLabel 	= transform.Find("bg").Find("lv").GetComponent<UILabel>();
		lifeSprite 	= transform.Find("bg").Find("life").GetComponent<UISprite>();
		nameLabel 	= transform.Find("name").GetComponent<UILabel>();
		basePos = transform.localPosition;
	}

	public void Init(InGameRole role){
		this.role = role;
		nameLabel.text = role.data.name;
		levelLabel.text = role.data.level + "";
		lifeSprite.transform.localScale = new Vector3(1,1,0);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void InGameUIPlayerInfoUnitUpdate () {
		levelLabel.text = role.data.level + "";


		//血条
		float fromval = lifeSprite.transform.localScale.x;
		float toval = (float)role.life / (float)role.maxlife;
		float addval = (fromval - toval) * 0.9f;

		lifeSprite.transform.localScale = new Vector3(toval + addval,1,0);

		//更新位置
		if(Camera.main == null || UICamera.currentCamera == null){
			return;
		}

		transform.position = GameCommon.WorldPosToNGUIPos(Camera.main,UICamera.currentCamera,role.transform.position) ;//+ basePos;
		transform.localPosition += basePos;
	}
}
