using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMe : MonoBehaviour
{
    private Transform _trans;

    private void Start()
    {
        _trans = transform;
    }

    private void FixedUpdate()
    {
        transform.position = _trans.position;
    }
}
