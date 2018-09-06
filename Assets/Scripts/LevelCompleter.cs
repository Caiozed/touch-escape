using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleter : MonoBehaviour {

	PlayerController player;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	public void CompleteLevel(){
		player.levelFinished = true;
		GoogleAds.ShowRewardedVideo();
	}
}
