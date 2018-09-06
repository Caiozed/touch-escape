using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    PlayerController player;
    public int battery = 1;
    AudioSource pickupAudio;
    public enum KeyColor
    {
        Red,
        Blue,
        Yellow
    };
    public KeyColor keyColor;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pickupAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Add battery to player 
    public void AddBattery()
    {
        if (player.battery < 3)
        {
            player.battery += battery;
            Deactivate();
        }
    }

    // Add Key to player inventory
    public void AddKey()
    {
        if(!player.keys.Contains(keyColor.ToString())){
            player.keys.Add(keyColor.ToString());
            Deactivate();
            //player.initialPosition = transform.position;
        };
    }

    void Deactivate(){
        pickupAudio.Play();
        GetComponent<Collider>().enabled = false;
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var item in renderers)
        {
            item.enabled = false;
        }
    }
}
