using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR;
using HurricaneVR.Framework.Core;




public class ProjectileBehavior : MonoBehaviour
{
    [HideInInspector]
    public EntitySpawner Spawner;



    private void OnCollisionEnter(Collision collision)
    {
        
        //if collision with the throwable food is with a Customer...
        //...then destroy the food and call the Customer Spawner's Despawn Satisfied method
        if(collision.gameObject.CompareTag("Customer"))
        {
            Spawner.DespawnSatisfiedEntity(collision.gameObject);
            Destroy(gameObject);
        }
    }


}
