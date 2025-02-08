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
        Instantiate(Product, transform.position, transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        collisions.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if(collisions.Contains(other.gameObject))
            collisions.Remove(other.gameObject);

        if(other.GetComponent<ProjectileBehavior>())
        {
            if(collisions.Count < 1)
                Instantiate(Product, transform.position, transform.rotation);
        }
    }


}
