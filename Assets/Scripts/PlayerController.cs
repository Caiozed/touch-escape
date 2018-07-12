using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerController : MonoBehaviour
{

    NavMeshAgent agent;
    public ParticleSystem touchEffect;
    Vector3 initialPosition, initialCameraRotation;
    ThirdPersonCharacter thirdPersonController;
    Touch[] touches;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public bool detected = false;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();
        agent.updateRotation = false;
        // agent.updatePosition = false;
        initialPosition = transform.position;
        initialCameraRotation = Camera.main.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Detected", detected);

        if (!detected)
        {
            // Camera.main.transform.eulerAngles = initialCameraRotation;
            // anim.SetFloat("Speed", agent.velocity.magnitude);
            RaycastHit hit;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(touchRay, out hit))
                {
                    agent.SetDestination(hit.point);
                    touchEffect.transform.position = hit.point;
                    touchEffect.Play();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out hit))
                {
                    agent.SetDestination(hit.point);
                    touchEffect.transform.position = hit.point;
                    touchEffect.Play();
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
    }

    // Restart player to last checkpoint
    public void Restart()
    {
        transform.position = initialPosition;
        agent.SetDestination(initialPosition);
        detected = false;
    }
}
