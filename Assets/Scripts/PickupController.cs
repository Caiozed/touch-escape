using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    PlayerController player;
    public int battery = 1;
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
            this.gameObject.SetActive(false);
        }
    }

    // Add Key to player inventory
    public void AddKey()
    {
        if(!player.keys.Contains(keyColor.ToString())){
            player.keys.Add(keyColor.ToString());
            this.gameObject.SetActive(false);
            player.initialPosition = transform.position;
        };
    }
}
