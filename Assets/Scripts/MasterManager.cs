using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour {
	Animator anim;

	GameObject[] enemiesObjects;
	GameObject player;
	EnemyController[] enemiesControllers;
	[HideInInspector]
	public bool restartTrigger;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		enemiesObjects = GameObject.FindGameObjectsWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player");
		enemiesControllers = new EnemyController[enemiesObjects.Length];
		for (int i = 0; i < enemiesObjects.Length; i++)
		{
			enemiesControllers[i] = (enemiesObjects[i].GetComponent<EnemyController>());
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(restartTrigger){
			Restart();
		}
	}

	// Restart game to last checkpoint
	void Restart(){
		for (int i = 0; i < enemiesControllers.Length; i++)
		{
			enemiesControllers[i].Restart();
		}
		player.GetComponent<PlayerController>().Restart();
		restartTrigger = false;
	}
}
