
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteractTrigger : MonoBehaviour
{

    // Use this for initialization
    public bool isPlayerNear = false;

    public int interactionCost = 1;
    [HideInInspector]
    public bool interactable = true;
    [HideInInspector]
    public Animator anim;
    public UnityEvent TriggerEvent;
    public GameObject target;
    public GameObject effect;
    LineRenderer line;
    public enum InteractableType
    {
        Console,
        Barrier
    }
    public InteractableType interactableType;
    public enum BarrierColor
    {
        Red,
        Blue,
        Yellow
    }
    public BarrierColor barrierColor;
    void Start()
    {
        anim = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (interactableType.ToString())
        {
            // Interactable controller
            case "Console":
                // Enable line if player is near and the object is interactable
                line.enabled = isPlayerNear && interactable;

                // If player is near draw line 
                if (isPlayerNear)
                {
                    line.SetPosition(0, transform.position + new Vector3(0, 0.5f, 0));
                    line.SetPosition(1, target.transform.position);
                }

                // Activa image assets
                effect.SetActive(isPlayerNear);
                break;

            default:
                return;
        }

    }

    // Deactivate barrier if player has key  
    public void DeactivateBarrier()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (player.keys.Contains(barrierColor.ToString()))
        {
            target.SetActive(false);
            BoxCollider[] boxColliders = GetComponents<BoxCollider>();
            for (int i = 0; i < boxColliders.Length; i++)
            {
                boxColliders[i].enabled = false;
            }
        }
    }
}
