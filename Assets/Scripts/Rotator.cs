using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	public Vector3 velocityVector;
	public float speed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(velocityVector * speed * Time.deltaTime * 60);
	}
}
