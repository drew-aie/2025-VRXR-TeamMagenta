using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR;
using HurricaneVR.Framework.Core;




public class ProjectileBehavior : MonoBehaviour
{
    public EntitySpawner Spawner;
    public HVRGrabbable GrabScript;


    private void OnTriggerEnter(Collider other)
    {
        GrabScript.Collided.AddListener();
    }
}
