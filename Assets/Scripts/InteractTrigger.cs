using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteractTrigger : MonoBehaviour {

	// Use this for initialization
	public bool isPlayerNear = false;
	public UnityEvent TriggerEvent;
	public GameObject effect;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		effect.SetActive(isPlayerNear);
	}
}
