using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
	AsyncOperation async;
	static int levelToLoad;
	// Use this for initialization
	void Start () {
		async = SceneManager.LoadSceneAsync (levelToLoad);
		async.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (async.progress == 0.9f) {
			async.allowSceneActivation = true;
		}
	}

	public static void LoadLevel(int level){
		levelToLoad = level;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.sceneCountInBuildSettings-1);
	}

	public static int CurrentLevel(){
		return SceneManager.GetActiveScene ().buildIndex;
	}
}
