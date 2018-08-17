using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
	public Image fadeImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void StartGame(){
		StartCoroutine("StartGameEnum");
	}
	IEnumerator StartGameEnum(){
		 Color color = new Color(0, 0, 0, 0.01f);

        fadeImage.color = color;
		fadeImage.CrossFadeAlpha(255f, 2f, false);
		yield return new WaitForSeconds(2);
		LoadingManager.LoadLevel(LoadingManager.CurrentLevel()+1);
	}
}
