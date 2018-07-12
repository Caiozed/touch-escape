using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Enemy") || other.CompareTag("Player")){
			Debug.Log("TEste");
			Open();
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Enemy") || other.CompareTag("Player")){
			Close();
		}
	}

	public void Open(){
		anim.SetBool("character_nearby", true);
	}
	public void Close(){
		anim.SetBool("character_nearby", false);
	}
}
