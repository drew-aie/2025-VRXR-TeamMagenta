using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMe : MonoBehaviour
{
    private Transform _original;

    private void Start()
    {
        _original = transform;
    }

    private void Update()
    {
        transform.position = _original.position;
    }
}
