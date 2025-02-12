using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR;
using HurricaneVR.Framework.Core;




public class ProjectileBehavior : MonoBehaviour
{
    public EntitySpawner Spawner;



    private void OnTriggerEnter(Collider other)
    {
        //get customer 
        // check tag 
        ///food despawn
        ///tell customer to despawn via satisfied function
        ///
        if(other.CompareTag("Customer"))
        {
            
            Destroy(gameObject);
            Spawner.DespawnSatisfiedEntity(other.gameObject);
        }
    }


}
