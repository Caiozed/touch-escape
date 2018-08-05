using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
    public Image fadeImage;
    public Image[] batteryIcons;
    public float minutesToFinish;
    public Text displayTime;
    public GameObject gameOverEffect, gameOverText;
    float timeToFinish;
    Animator anim;
    GameObject[] enemiesObjects;
    public EZCameraShake.CameraShaker cameraShake;
    GameObject player;
    PlayerController playerController;
    EnemyController[] enemiesControllers;
    [HideInInspector]
    public bool restartTrigger, gameOver;
    // Use this for initialization
    void Start()
    {
        timeToFinish = minutesToFinish * 60;
        timeToFinish -= 50;
        anim = GetComponent<Animator>();
        enemiesObjects = GameObject.FindGameObjectsWithTag("Enemy");

        DateTime time = new DateTime((long)Time.fixedTime);

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
    void FixedUpdate()
    {
        DrawBattery();
        if (restartTrigger)
        {
            StartCoroutine("Restart");
        }

        if (timeToFinish <= 0 && !gameOver)
        {
            StartCoroutine("GameOver");
        }
        else
        {
            DrawTimer();
        }
    }

    // Restart game to last checkpoint
    IEnumerator Restart()
    {
        Color color = new Color(0, 0, 0, 0.01f);

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

    IEnumerator GameOver()
    {
        displayTime.enabled = false;
        gameOver = true;
        cameraShake.ShakeOnce(15, 5, 0, 1);
        Instantiate(gameOverEffect, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        gameOverText.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        LoadingManager.LoadLevel(LoadingManager.CurrentLevel());
    }

    void DrawBattery()
    {
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

    void DrawTimer()
    {
        timeToFinish -= Time.deltaTime;
        string minutes = Mathf.Floor(timeToFinish / 60).ToString("00");
        string seconds = (timeToFinish % 60).ToString("00");
        // minutes = Mathf.Clamp(minutes, 0, 60);
        // seconds = Mathf.Clamp(seconds, 0, 60);
        displayTime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

}
