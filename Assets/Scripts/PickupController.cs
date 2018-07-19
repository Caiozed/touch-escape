using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    PlayerController player;
    public int battery = 1;
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
            Destroy(this.gameObject);
        }
    }
}
