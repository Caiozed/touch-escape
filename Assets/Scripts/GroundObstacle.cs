using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacle : MonoBehaviour {

ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			ps.Play();
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
		}
	}
}
