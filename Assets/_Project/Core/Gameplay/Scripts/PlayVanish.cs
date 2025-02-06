using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVanish : MonoBehaviour
{
    private void Awake()
    {
       MeshRenderer renderer = GetComponent<MeshRenderer>();
       if (renderer != null)
            GetComponent<MeshRenderer>().enabled = false;
    }
}
