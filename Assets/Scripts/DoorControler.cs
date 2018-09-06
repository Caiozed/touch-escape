using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{

    Animator anim;
    AudioSource openAudio;
    bool isAudioPlaying;
    // Use this for initialization
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        openAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            if (!isAudioPlaying)
            {
                openAudio.Play();
            }
            Open();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            openAudio.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Close();
        }
    }

    public void Open()
    {
        anim.SetBool("character_nearby", true);
        isAudioPlaying = true;
    }
    public void Close()
    {
        anim.SetBool("character_nearby", false);
        isAudioPlaying = false;
    }
}
