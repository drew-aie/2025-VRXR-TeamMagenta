using HurricaneVR.Framework.Weapons.Bow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntitySpawner : MonoBehaviour
{
    public GameObject EntitySpawnPrefab;

    private GameObject _lastSpawned;
    private Quaternion _spawnRotation;

    [HideInInspector]
    public List<GameObject> SpawnedObjects;
    [HideInInspector]
    public List<GameObject> DespawnedObjects;
    [SerializeField]
    private List<LanePath> _paths;
    [HideInInspector]
    public int TriggerCount;

    private void Awake()
    {
        SpawnedObjects = new List<GameObject>();
        DespawnedObjects = new List<GameObject>();
        _spawnRotation = transform.rotation;
    }

    public GameObject Spawn()
    {
        if(EntitySpawnPrefab == null)
        {
            return null;
        }
        Vector3 pos = transform.position;
        if (DespawnedObjects != null
            && DespawnedObjects.Count > 0)
        {
            GameObject ToSpawn = DespawnedObjects[0];
            DespawnedObjects.Remove(ToSpawn);
            _lastSpawned = ToSpawn;
            _lastSpawned.SetActive(true);

            transform.position = pos;
            transform.rotation = _spawnRotation;

            NavMeshAgent navBehavior = _lastSpawned.GetComponent<NavMeshAgent>();
            if (navBehavior)
                navBehavior.enabled = true;
        }
        else
        {
            _lastSpawned = Instantiate(EntitySpawnPrefab, pos, _spawnRotation);
        }

        CustomerBehavior behavior = _lastSpawned.GetComponent<CustomerBehavior>();
        behavior.Paths = _paths;
        behavior.InitOrRefresh();
        
        SpawnedObjects.Add(_lastSpawned);

        return _lastSpawned;
    }

    public bool DespawnOwnedEntity(GameObject obj)
    {
        if (SpawnedObjects.Contains(obj))
        {
            SpawnedObjects.Remove(obj);
            obj.SetActive(false);
            DespawnedObjects.Add(obj);

            return true;
        }
        else return false;
    }

    public void DespawnAllEntities()
    {
        for(int i = 0; i < SpawnedObjects.Count; i++)
        {
            GameObject obj = SpawnedObjects[i];
            DespawnOwnedEntity(obj);
        }

        DespawnedObjects.RemoveRange(0, DespawnedObjects.Count - 1);
        SpawnedObjects.RemoveRange(0, SpawnedObjects.Count - 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
            TriggerCount++;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
            TriggerCount--;
        if(TriggerCount < 0)
            TriggerCount = 0;
    }
}
