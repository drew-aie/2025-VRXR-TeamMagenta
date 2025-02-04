using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LanePath : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPoint;
    [SerializeField]
    private GameObject _endPoint;

    [HideInInspector]
    public Vector3 Start { get => _startPoint.transform.position; } 
    [HideInInspector]
    public Vector3 End { get => _endPoint.transform.position; } 

    public Vector3 Direction 
    {
        get => (End - Start).normalized;
    }
}
