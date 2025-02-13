using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR;
using HurricaneVR.Framework.Core;




public class ProjectileBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if collision with the throwable food is with a Customer...
        //...then destroy the food and call the Customer Spawner's Despawn Satisfied method
        if(other.TryGetComponent(out CustomerBehavior behavior))
        {
            behavior.DespawnSatisfied();
            Destroy(gameObject);
        }
    }


}
