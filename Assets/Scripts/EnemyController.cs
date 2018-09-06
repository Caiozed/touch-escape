using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{

    public float speed = 5;
    public int xDistance = 2;
    [HideInInspector]
    public bool playerDetected;
    public GameObject alertObject;
    Vector3 nextPosition, initialPosition;
    List<Transform> positions;
    NavMeshAgent agent;
    MeshCollider col;
    Transform chaseTarget;
    ThirdPersonCharacter thirdPersonController;
    GameObject player;
    Animator anim;
    MasterManager gameManager;
    int posIndex = 0;
    bool isAudioPlaying;
    AudioSource detectionSound;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponentInChildren<MeshCollider>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();
        detectionSound = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        positions = GetComponentInParent<PathGenerator>().Positions;
        initialPosition = transform.position;

        NextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDetected)
        {
            if (chaseTarget)
            {
                agent.SetDestination(chaseTarget.position);
            }
            else
            {
                if (Vector3.Distance(transform.position, nextPosition) < 0.5f)
                {
                    NextPosition();
                }
            }

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                thirdPersonController.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                thirdPersonController.Move(Vector3.zero, false, false);
            }
        }

        // var nav = GetComponent<NavMeshAgent>();
        // if (nav == null || nav.path == null)
        //     return;

        // var line = this.GetComponent<LineRenderer>();
        // if (line == null)
        // {
        //     line = this.gameObject.AddComponent<LineRenderer>();
        //     line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
        //     line.startWidth = 0.2f;
        //     line.endWidth = 0.2f;
        //     line.startColor = Color.yellow;
        //     line.endColor = Color.yellow;
        // }

        // var path = nav.path;

        // line.positionCount = path.corners.Length;

        // for (int i = 0; i < path.corners.Length; i++)
        // {
        //     line.SetPosition(i, path.corners[i]);
        // }

    }

    // Moves enemy to nextPosition on list
    void NextPosition()
    {
        if (positions.Count > 0)
        {
            // Set next position to move
            nextPosition = positions[posIndex].position;
            agent.SetDestination(nextPosition);
            posIndex++;

            // Reverse direction if arrived at last position available
            if (posIndex > positions.Count - 1)
            {
                positions.Reverse();
                posIndex = 0;
            }
        }
    }

    // Tell enemy to chase player
    public void SetChaseTarget()
    {
        alertObject.SetActive(true);

        if (!isAudioPlaying)
        {
            detectionSound.Play();
            isAudioPlaying = true;
        }

        // Stop Enemy movement
        agent.isStopped = true;
        playerDetected = true;
        thirdPersonController.Move(Vector3.zero, false, false);

        // Start restart
        gameManager.restartTrigger = true; //Set master manager trigger to restart game upon detection
                                           // chaseTarget = player.transform;
    }

    // Restart enemy to its initial position and beahavior
    public void Restart()
    {
        // Restart player
        playerDetected = false;
        isAudioPlaying = false;

        alertObject.SetActive(false);
        agent.isStopped = false;
        transform.position = initialPosition; //Reset position to initial position
        posIndex = 0; //Reset position Index
        chaseTarget = null; //Reset chase target
        anim.SetBool("DetectionTrigger", false);        // Play detection animation
    }

    // 	 void OnDrawGizmosSelected()
    //  {

    //      var nav = GetComponent<NavMeshAgent>();
    //      if( nav == null || nav.path == null )
    //          return;

    //      var line = this.GetComponent<LineRenderer>();
    //      if( line == null )
    //      {
    //          line = this.gameObject.AddComponent<LineRenderer>();
    //          line.material = new Material( Shader.Find( "Sprites/Default" ) ) { color = Color.yellow };
    //          line.startWidth = 0.5f;
    // 		 line.endWidth = 0.5f;
    //          line.startColor = Color.yellow;
    // 		 line.endColor = Color.yellow;
    //      }

    //      var path = nav.path;

    //      line.positionCount = path.corners.Length;

    //      for (int i = 0; i < path.corners.Length - 1; i++)
    // 	{
    // 		Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    // 	}

    //  }
}
