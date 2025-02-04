using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVanish : MonoBehaviour
{
    private void Awake()
    {
       GetComponent<MeshRenderer>().enabled = false;
    }
}
