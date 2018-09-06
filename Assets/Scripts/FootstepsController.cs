using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsController : MonoBehaviour
{

    public AudioClip[] sounds;
    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            audioSource.clip = sounds[Random.Range(0, sounds.Length - 1)];
            audioSource.Play();
        }
    }
}
