using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        Camera.main.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+10, target.transform.position.z);
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
