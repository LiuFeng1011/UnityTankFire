using UnityEngine;
using System.Collections;
using System.Runtime.Hosting;
using UnityEngine.SceneManagement;

public class InitializeOnLoad : MonoBehaviour {

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		string getname =  ChangeScene.SceneTag.GetName(typeof(ChangeScene.SceneTag), ChangeScene.SceneTag.Gate);
		if (SceneManager.GetActiveScene().name == getname || SceneManager.GetActiveScene().name == "Test" )
		{
			return;
		}
		ChangeScene.ExchangeScene(ChangeScene.SceneTag.Gate);
	}
}
