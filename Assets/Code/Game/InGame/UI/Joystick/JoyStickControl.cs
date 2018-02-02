using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickControl : MonoBehaviour {

	public static JoyStickControl instance = null;

	private float parentX = 0;
	private float parentY = 0;

	private bool isPress=false;

	UISprite parentSpirite;

	void Awake()
	{
		instance = this;
		parentSpirite = transform.parent.GetComponent<UISprite>();
	}

	/// <summary>
	/// 设置摇杆坐标
	/// </summary>
	/// <param name="pos">Position.</param>
	public void SetPosition(Vector3 pos){
		parentSpirite.transform.position = pos;
	}

	/// <summary>
	/// 获取数据.
	/// </summary>
	/// <returns>The vector.</returns>
	public Vector3 GetVector(){
		return transform.localPosition.normalized;
	}

	//是否操作
	public bool IsPress(){
		return isPress;
	}
		
	void OnDrag(Vector2 delta)  
	{  
		if(parentX == 0){
			Vector3 parentpos = UICamera.currentCamera.WorldToScreenPoint(parentSpirite.transform.position);
			parentX = parentpos.x;
			parentY = parentpos.y;
		}

		Vector2 touchpos = UICamera.lastTouchPosition;

		touchpos -= new Vector2(parentX, parentY);

		if(Vector2.Distance(Vector2.zero,touchpos) > 500){
			return;
		}
		// 计算原点和触摸点的距离
		float distance = Vector2.Distance(touchpos, Vector2.zero);
		if(distance < 53)// 距离在父精灵背景中圆内，53为其半径
		{
			transform.localPosition = touchpos;
		}
		else
		{//  触摸点到原点的距离超过半径，则把子精灵按钮的位置设置为在父精灵背景的圆上，即控制摇杆只能在父精灵圆内移动
			transform.localPosition = touchpos.normalized * 53;
		}
	}
	// 触摸按下，isPress为true；抬起为false
	void OnPress(bool isPress)
	{
//		Debug.Log("UICamera.currentTouchID : " + UICamera.currentTouchID);
//		// 保存标志位：按下或抬起，用于在Update方法中判断触摸是否按下
		this.isPress = isPress;
		if(!isPress){
			transform.localPosition = Vector2.zero;
		}
	}


	void Destroy(){
		instance = null;
	}
}
