﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public Image fadeImage;
    public Image[] batteryIcons;
    Animator anim;
    GameObject[] enemiesObjects;
    GameObject player;
    PlayerController playerController;
    EnemyController[] enemiesControllers;
    [HideInInspector]
    public bool restartTrigger = false;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        enemiesObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // Player references
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        enemiesControllers = new EnemyController[enemiesObjects.Length];
        for (int i = 0; i < enemiesObjects.Length; i++)
        {
            enemiesControllers[i] = (enemiesObjects[i].GetComponent<EnemyController>());
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        DrawBattery();
        if (restartTrigger)
        {
            StartCoroutine("Restart");
        }
    }

    // Restart game to last checkpoint
    IEnumerator Restart()
    {
		Color color = new Color(0,0,0,0.01f);

		fadeImage.color = color;
		fadeImage.CrossFadeAlpha(255f, 1f, false);

        playerController.detected = true;

        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < enemiesControllers.Length; i++)
        {
            enemiesControllers[i].Restart();
        }

        fadeImage.CrossFadeAlpha(0, 1.0f, false);
        restartTrigger = false;
        playerController.Restart();
    }

    void DrawBattery(){
         for (int i = 0; i < batteryIcons.Length; i++)
        {
            batteryIcons[i].enabled = false;
        }

         // Handle battery itens
         for (int i = 0; i < playerController.battery; i++)
        {
            batteryIcons[i].enabled = true;
        }
    }
}
