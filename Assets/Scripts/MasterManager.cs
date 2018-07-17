using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public Image fadeImage;
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
		fadeImage.CrossFadeAlpha(255f, 2f, false);

        playerController.detected = true;

        yield return new WaitForSeconds(2);

        for (int i = 0; i < enemiesControllers.Length; i++)
        {
            enemiesControllers[i].Restart();
        }

        fadeImage.CrossFadeAlpha(0, 2.0f, false);
        playerController.Restart();
        restartTrigger = false;
    }
}
