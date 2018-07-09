using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerDetection : MonoBehaviour {

    public UnityEvent onTrigger;
    public string objectTag;
	void OnTriggerEnter(Collider other){
		if(other.transform.CompareTag(objectTag)){
			onTrigger.Invoke();
		}
	}
}
