using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TriggerCustomerDespawn : MonoBehaviour
{
    public UnityEvent OnTriggerDespawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CustomerBehavior behavior))
        {
            behavior.DespawnSatisfied();
            OnTriggerDespawn.Invoke();
        }
    }
}
