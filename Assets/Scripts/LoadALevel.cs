using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadALevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(int levelToLoad){
		LoadingManager.LoadLevel(levelToLoad);
	}
}

