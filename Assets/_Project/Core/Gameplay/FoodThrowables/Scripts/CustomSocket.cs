using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSocket : MonoBehaviour
{
    public GameObject Product;

    private List<GameObject> collisions;

    private void Awake()
    {
        collisions = new List<GameObject>();
        Instantiate(Product, transform.position, transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        collisions.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!collisions.Remove(other.gameObject))
            return;
        if(collisions.Count < 1)
           Instantiate(Product, transform.position, transform.rotation);
        
    }


}
