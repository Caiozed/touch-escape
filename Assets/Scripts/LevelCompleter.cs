using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleter : MonoBehaviour {

	public Sprite warningImage;
	public Sprite successImage;
	public Image currentImage;
	PlayerController player;

	void Start(){
		currentImage.color = Color.red;
		currentImage.sprite = warningImage;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	public void CompleteLevel(){
		currentImage.color = Color.green;
		currentImage.sprite = successImage;
		player.levelFinished = true;
	}
}
