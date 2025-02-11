using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TagTriggerDespawn : MonoBehaviour
{
    public string TagToCompare;
    [SerializeField]
    private EntitySpawner _spawner;
    public UnityEvent OnTriggerDespawn;

    private void OnTriggerEnter(Collider other)
    {
        if(!_spawner) return;

        if (other.CompareTag(TagToCompare))
        {
            //waits until animation clip is complete.
            _spawner.DespawnEnragedEntity(other.gameObject);
            OnTriggerDespawn.Invoke();
        }
    }
}
