using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	[HideInInspector]
	public bool isPaused;
	public Sprite playImage, pauseImage;
	public GameObject pauseScreen;
	Image imageContainer;
	// Use this for initialization
	void Start () {
		imageContainer = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PauseToggle(){
		isPaused = !isPaused;
		pauseScreen.SetActive(isPaused);
		if(isPaused){
			imageContainer.sprite = playImage;
			Time.timeScale = 0;
		}else{
			imageContainer.sprite = pauseImage;
			Time.timeScale = 1;
		}
	}
}
