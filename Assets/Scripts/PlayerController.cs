using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerController : MonoBehaviour
{

    NavMeshAgent agent;
    public ParticleSystem touchEffect;
    public int battery = 3;
    [HideInInspector]
    public Vector3 initialPosition, initialCameraRotation;
    ThirdPersonCharacter thirdPersonController;
    InteractTrigger interactionTrigger;
    MasterManager masterManager;
    Touch[] touches;
    [HideInInspector]
    public List<string> keys;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public bool detected = false, isNearInteractable = false, levelFinished = false;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();
        masterManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterManager>();
        agent.updateRotation = false;
        // agent.updatePosition = false;
        initialPosition = transform.position;
        initialCameraRotation = Camera.main.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Detected", detected);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!detected && !levelFinished && !masterManager.gameOver)
        {
            // Camera.main.transform.eulerAngles = initialCameraRotation;
            // anim.SetFloat("Speed", agent.velocity.magnitude);
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(touchRay, out hit))
                {
                    if (hit.transform.CompareTag("Interactable") && isNearInteractable)
                    {
                        if (interactionTrigger.interactionCost <= battery && interactionTrigger.interactable)
                        {
                            interactionTrigger.TriggerEvent.Invoke();
                            interactionTrigger.target.GetComponent<AudioSource>().Play();
                            battery -= interactionTrigger.interactionCost;
                            interactionTrigger.interactable = false;
                        }
                        else if (interactionTrigger.interactable && interactionTrigger.interactionCost >= battery)
                        {
                            interactionTrigger.anim.SetTrigger("BatteryNotEnough");
                        }
                    }
                    else
                    {
                        agent.SetDestination(hit.point);
                        touchEffect.transform.position = hit.point;
                        touchEffect.Play();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(mouseRay, out hit))
                {
                    if (hit.transform.CompareTag("Interactable") && isNearInteractable)
                    {
                        if (interactionTrigger.interactionCost <= battery && interactionTrigger.interactable)
                        {
                            interactionTrigger.TriggerEvent.Invoke();
                            interactionTrigger.target.GetComponent<AudioSource>().Play();
                            battery -= interactionTrigger.interactionCost;
                            interactionTrigger.interactable = false;
                        }
                        else if (interactionTrigger.interactable && interactionTrigger.interactionCost >= battery)
                        {
                            interactionTrigger.anim.SetTrigger("BatteryNotEnough");
                        }
                    }
                    else
                    {
                        agent.SetDestination(hit.point);
                        touchEffect.transform.position = hit.point;
                        touchEffect.Play();
                    }
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
        else
        {
            agent.ResetPath();
        }

        if (levelFinished)
        {
            agent.speed = 0;
            thirdPersonController.Move(Vector3.zero, false, false);
        }
    }

    // Restart player to last checkpoint
    public void Restart()
    {
        transform.position = initialPosition;
        agent.SetDestination(initialPosition);
        agent.ResetPath();
        detected = false;
    }


    void OnTriggerEnter(Collider other)
    {
        // Set interactable reference when near
        if (other.CompareTag("Interactable"))
        {
            isNearInteractable = true;
            interactionTrigger = other.gameObject.GetComponent<InteractTrigger>();
            interactionTrigger.isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //  Destroy interactable reference when too far
        if (other.CompareTag("Interactable"))
        {
            interactionTrigger.isPlayerNear = false;
            isNearInteractable = false;
            interactionTrigger = null;
        }
    }
}
