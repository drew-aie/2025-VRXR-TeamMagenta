using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagTriggerFeed : MonoBehaviour
{
    public string TagToCompare;
    [SerializeField]
    private EntitySpawner _spawner;
    public UnityEvent OnFed;

    private void OnTriggerEnter(Collider other)
    {
        if (!_spawner) return;
        if (other.CompareTag(TagToCompare) || TagToCompare.Length < 1)
        {
            //waits until animation clip is complete.
            _spawner.DespawnSatisfiedEntity(other.gameObject);
            OnFed.Invoke();

            //despawn this to the projectile spawn pool.
            //destruction is temporary.
            Destroy(this);
        }
    }
}
