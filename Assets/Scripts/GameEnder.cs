using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GameEnder : MonoBehaviour
{
	public GameObject player;
	public ParticleSystem engineEffect;
	public Camera creditsCamera;
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
		GetComponent<Animator>().enabled = true;

	}

	public void ActivateCamera(){
		creditsCamera.enabled = true;
			Debug.Log(creditsCamera.enabled);

		Camera.main.enabled = false;
	}
}
