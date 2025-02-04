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
    public UnityEvent OnDespawn;

    private void OnTriggerEnter(Collider other)
    {
        if(!_spawner) return;
        if (other.CompareTag(TagToCompare) || TagToCompare.Length < 1)
        {
            other.gameObject.transform.position = _spawner.transform.position;

            _spawner.DespawnOwnedEntity(other.gameObject);
            OnDespawn.Invoke();
            Debug.Log("Despawned");
        }
        
    }

    
}
