using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PathGenerator : MonoBehaviour
{

    [SerializeField]
    private List<Transform> positions;

    public List<Transform> Positions
    {
        get
        {
            return positions;
        }
        set
        {
            positions = value;
        }
    }
}
