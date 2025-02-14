using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFeed : MonoBehaviour
{
    public UnityEvent OnFed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CustomerBehavior behavior))
        {
            //waits until animation clip is complete.
            behavior.DespawnSatisfied();
            OnFed.Invoke();

            Destroy(this);
        }
    }
}
