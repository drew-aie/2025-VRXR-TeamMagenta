using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSocket : MonoBehaviour
{
    public GameObject Product;


    private void Awake()
    {
        Instantiate(Product, transform.position, transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<ProjectileBehavior>())
            Instantiate(Product, transform.position, transform.rotation);
    }


}
