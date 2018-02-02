using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMonitorData{
	string name;
	long usetime;

	public TimeMonitorData(string name,long usetime){
		this.name = name;
		this.usetime = usetime;
	}

	public void Log(){
		Debug.Log("unit name : " + name + "  usetime : " + usetime);
	}
}

/// <summary>
/// author:liufeng
/// date:170809
/// 游戏时间监测
/// </summary>
public class TimeMonitor {
	bool isStart = false;

	long startTime;
	long lastTime;

	List<TimeMonitorData> datalist = new List<TimeMonitorData>();

	int standardTimeLong;
	string title;

	//standardTimeLong超过此数值会出log
	public TimeMonitor(int standardTimeLong,string title = ""){
		this.standardTimeLong = standardTimeLong;
		this.title = title;
	}

	public void Start(){
		datalist.Clear();
		startTime = GameCommon.GetTimeStamp(false);
		lastTime = startTime;
		isStart = true;
	}

	public void Step(string name){
		if(!isStart){
			return;
		}
		long time = GameCommon.GetTimeStamp(false);

		TimeMonitorData data = new TimeMonitorData(name,time-lastTime);
		datalist.Add(data);

		lastTime = time;
	}

	public void Stop(){
		if(!isStart){
			return;
		}
		isStart = false;
		lastTime = GameCommon.GetTimeStamp(false);

		//用时过长
		if(lastTime - startTime > standardTimeLong){
			Debug.Log("============ " +title+ "Time tooooo long!!===========");
			Debug.Log("总时长:" + (lastTime - startTime));
			for(int i = 0 ; i < datalist.Count ; i ++){
				datalist[i].Log();
			}
		}
	}

}
