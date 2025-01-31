using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePath : MonoBehaviour
{
    public GameObject Start;
    public GameObject End;

    [SerializeField]
    private bool _customDirection;
    [SerializeField]
    private Vector3 _direction;
    public Vector3 Direction 
    {
        get => _customDirection ? _direction : (End.transform.position - Start.transform.position).normalized;
    }
}
