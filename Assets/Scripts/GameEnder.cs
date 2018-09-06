using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GameEnder : MonoBehaviour
{
	public GameObject player;
	public ParticleSystem engineEffect;
	public Camera creditsCamera;
	public MasterManager masterManager;
	public Canvas  canvasToDisable, canvasToEnable;
    // Use this for initialization

    public void EndGame()
    {
		engineEffect.Play();
		player.transform.position = gameObject.transform.position;
		player.transform.SetParent(gameObject.transform);
		player.transform.localScale = new Vector3(.5f, .5f, .5f);
		player.GetComponent<Collider>().enabled = false;
		player.GetComponent<NavMeshAgent>().enabled = false;
		player.GetComponent<PlayerController>().enabled = false;
		player.GetComponent<Rigidbody>().isKinematic = true;
		player.GetComponentInChildren<Light>().enabled = false;
		GetComponent<Animator>().enabled = true;
		masterManager.minutesToFinish = 100000000000000000;
	}

	public void ActivateCamera(){
		creditsCamera.enabled = true;
		canvasToDisable.enabled = false;
		canvasToEnable.enabled = true;
		Camera.main.enabled = false;
	}
}
