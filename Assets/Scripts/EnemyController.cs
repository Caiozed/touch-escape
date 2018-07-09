using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{

    public Transform startPosition, endPosition;
    public float speed = 5;
    public int xDistance = 2;
    NavMeshAgent agent;
    MeshCollider col;
    Vector3 initialPosition;
    Transform chaseTarget;
    ThirdPersonCharacter thirdPersonController;

    GameObject player;
    Animator anim;
    MasterManager gameManager;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponentInChildren<MeshCollider>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();

        agent.SetDestination(startPosition.position);
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (chaseTarget)
        {
            agent.SetDestination(chaseTarget.position);
        }
        else
        {

            if (Vector3.Distance(transform.position, startPosition.position) < 0.5f)
            {
                agent.SetDestination(endPosition.position);
            }
            else if (Vector3.Distance(transform.position, endPosition.position) < 0.5f)
            {
                agent.SetDestination(startPosition.position);
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

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
            line.startColor = Color.yellow;
            line.endColor = Color.yellow;
        }

        var path = nav.path;

        line.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }

    // Tell enemy to chase player
    public void SetChaseTarget()
    {
        anim.SetBool("DetectionTrigger", true);
        gameManager.restartTrigger = true; //Set master manager trigger to restart game upon detection
                                           // chaseTarget = player.transform;
    }

    // Restart enemy to its initial position and beahavior
    public void Restart()
    {
        transform.position = initialPosition; //Reset position to initial position
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
