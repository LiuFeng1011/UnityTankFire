using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170807
/// 游戏内玩家
/// </summary>
public class InGameRole : InGameBaseObject {

	public enum RoleState{
		die,
		fight,
	}

	Dictionary<int,BaseSkill> skillList = new Dictionary<int,BaseSkill>();

	public InGameRoleData data{get;private set;}

	public RoleState state{get;private set;}

	public BaseWeapon weapon{get;private set;}
	AbsorbItem absorb;
	tank_list conf;

	public int scores{get;private set;}
	public int life{get;private set;}
	public int maxlife{get;private set;}

	//死亡时间
	long deathTime = 0;
	//死亡次数
	int deathCount = 0;

	float targetRotate = 0;

	GameObject firePoint;

	TimeMonitor timeMonitor;

	PlayerBuffManager playerBuffManager;

	Rigidbody rigidbody;

	public void Init(InGameRoleData data){
		this.data = data;
		conf = ConfigManager.tankListManager.GetDataById(data.tankid);
		maxlife = conf.tank_hp;
		life = maxlife;
		state = RoleState.fight;

		weapon = BaseWeapon.CreateWeapon(3001001);
		absorb = new AbsorbItem(this);

		rigidbody = transform.GetComponent<Rigidbody>();

		firePoint = transform.Find("firePoint").gameObject;

		timeMonitor = new TimeMonitor(5,"InGameRole");

		playerBuffManager = new PlayerBuffManager();

		//添加技能
		for(int i = 0 ; i < ConfigManager.skillListManager.datas.Count ; i ++){
			skillList.Add(ConfigManager.skillListManager.datas[i].skill_id,new BaseSkill(this,ConfigManager.skillListManager.datas[i].skill_id));
		}

	}



	public override ObjType GetType ()
	{
		return ObjType.role;
	}
	// Use this for initialization
	void Start () {
		
	}


	//旋转
	private void RotateUpdate(){
		if(Mathf.Abs(transform.eulerAngles.y - targetRotate) < 1) {
			return;
		}

		int param = -1;
		if((transform.eulerAngles.y < targetRotate && targetRotate - transform.eulerAngles.y < 180) || 
			(transform.eulerAngles.y > targetRotate && transform.eulerAngles.y - targetRotate > 180)){
			param = 1;
		}
		// 358 - 359 + 8
		float _t = 0;
		if(param > 0){
			_t = transform.eulerAngles.y + Time.deltaTime * GetRotateSpeed();

//			if(_t > 360){
//				_t-= 360;
//			}
			if(transform.eulerAngles.y < targetRotate && _t > targetRotate){
				_t = targetRotate;
			}
		}else{
			_t = transform.eulerAngles.y - Time.deltaTime * GetRotateSpeed();
//			if(_t < 0){
//				_t += 360;
//			}
			if(transform.eulerAngles.y > targetRotate && _t < targetRotate){
				_t = targetRotate;
			}
		}

		transform.eulerAngles = new Vector3(0,_t,0);

		if(Mathf.Abs(transform.eulerAngles.y - targetRotate) < Time.deltaTime * GetRotateSpeed()) {
			transform.eulerAngles = new Vector3(0,targetRotate,0);
		}

	}
	
	// Update is called once per frame
	public void RoleUpdate () {
		if(IsDie()){
			if(InGameManager.gameTime - deathTime > ConfigManager.normalLevelManager.data.Normal_level_die * 1000){
				Revive();
			}

			return;
		}

		timeMonitor.Start();
		//吸收道具
		absorb.AbsorbItemUpdate();
		timeMonitor.Step("absorb");

		//buff
		playerBuffManager.PlayerBuffManagerUpdate();
		timeMonitor.Step("buff");

		//旋转
		RotateUpdate();
		timeMonitor.Step("Rotate");

		//移动
		if(!this.IsCanMove()){
			Vector3 forward = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
			if(Vector3.Distance(forward,Vector3.zero) > 0.1){
				transform.forward = forward;
				targetRotate = transform.eulerAngles.y;
			}
		}
		this.transform.Translate(Vector3.forward*Time.deltaTime*GetSpeed());

		timeMonitor.Stop();
	}

	/// <summary>
	/// 增加一个buf
	/// </summary>
	/// <param name="buf">Buffer.</param>
	public void AddBuff(BaseBuff buf){
		playerBuffManager.AddBuff(buf);
	}

	/// <summary>
	/// 获取移动速度
	/// </summary>
	/// <returns>The speed.</returns>
	public float GetSpeed(){
		float addition = this.playerBuffManager.GetBufValByType(BaseBuff.BuffType.moveSpeed);
		return conf.tank_speed * (addition + 1);
	}
	/// <summary>
	/// 旋转速度
	/// </summary>
	/// <returns>The rotate speed.</returns>
	public int GetRotateSpeed(){
		return conf.tank_rotate_speed;
	}

	/// <summary>
	/// 开火
	/// </summary>
	public void Fire(){
		if(IsDie() ){
			return;
		}
		if(weapon == null) return;

		weapon.Fire(this,transform.position,transform.forward);

		if(weapon.IsUseless()){
			InGameManager.instance.playerManager.ChangeWeapon(this.data.id,3001001);
		}
	}

	//释放技能
	public void FireSkill(int skillId){

		if(!skillList.ContainsKey(skillId)){
			return;
		}
		skillList[skillId].Fire();
	}
	/// <summary>
	/// 旋转
	/// </summary>
	/// <param name="r">The red component.</param>
	public void Rotate(float r){

		//移动
		if(!IsCanMove()){
			return;
		}

		if(r > 360 ){
			r -= 360;
		}
		targetRotate = r;
	}

	public bool IsReturnBullet(){
		float addition = this.playerBuffManager.GetBufValByType(BaseBuff.BuffType.returnBullet);
		return addition > 0;
	}

	/// <summary>
	/// 血量改变
	/// </summary>
	/// <param name="val">增减量.</param>
	public void ChangeLife(int val,bool isforce = false){

		if(state == RoleState.die ){
			return;
		}

		if(val < 0 && !isforce){
			float addition = this.playerBuffManager.GetBufValByType(BaseBuff.BuffType.noEnemy);
			if(addition > 0){
				return;
			}
		}

		life += val;
		if(life > maxlife){
			life = maxlife;
		}

		if(life <= 0){
			Die();
		}
	}

	/// <summary>
	/// 切换武器
	/// </summary>
	public void ChangeWeapon(int weaponid){
		weapon = BaseWeapon.CreateWeapon(weaponid);
	}

	/// <summary>
	/// 获取子弹创建位置
	/// </summary>
	/// <returns>The fire point.</returns>
	public Vector3 GetFirePoint(){
		return this.firePoint.transform.position;
	}
	public Vector3 GetFireLocalPoint(){
		return this.firePoint.transform.localPosition;
	}

	public void AddScores(int s){
		this.scores += s;
	}
	//是否死亡
	public bool IsDie(){
		return state == RoleState.die;
	}

	//是否可以移动
	public bool IsCanMove(){
		float val = this.playerBuffManager.GetBufValByType(BaseBuff.BuffType.cantMove);
		if(val > 0) {
			return false;
		}
		return true;
	}

	//玩家碰撞
	public void HitPlayer(InGameRole role){
		BaseBuff buf = new BaseBuff(this,BaseBuff.BuffType.cantMove, 1,InGameManager.gameTime,conf.tank_uncontrolled);
		InGameManager.instance.playerManager.AddBuf(data.id, buf);

		//后退

		rigidbody.velocity = transform.forward*5;
	}


	/// <summary>
	/// 角色死亡
	/// </summary>
	private void Die(){
		state = RoleState.die;

		deathCount++;
		deathTime = InGameManager.gameTime;

		InGameManager.instance.gameEffectManager.AddWorldEffect(60010010,transform.position);

		transform.position = new Vector3 (10000,10000,10000);

		InGameManager.instance.playerManager.ChangeWeapon(this.data.id,3001001);

	}

	void OnDestroy(){
		playerBuffManager.ClearBuff();
	}

	/// <summary>
	/// 复活
	/// </summary>
	private void Revive(){
		state = RoleState.fight;
		life = maxlife;

		transform.position = InGameManager.instance.mapManager.GetRandPoint();
	}

	/// <summary>
	/// 吸金币距离
	/// </summary>
	/// <returns>The absorb distance.</returns>
	public float GetAbsorbDistance(){
		float ret = conf.tank_absorb;
		ret += this.playerBuffManager.GetBufValByType(BaseBuff.BuffType.absorb);
		return ret;
	}



	void OnCollisionEnter(Collision collision) 
	{
		InGameRole role = collision.transform.GetComponent<InGameRole>();
		if(role == null) {
			return;
		}
		//如果碰到敌人 无法移动
		InGameManager.instance.playerManager.HitPlayer(data.id,role.data.id);
	}
}
