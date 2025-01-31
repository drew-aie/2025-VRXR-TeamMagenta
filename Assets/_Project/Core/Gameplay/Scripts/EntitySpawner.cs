using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject EntitySpawnPrefab;

    
    [SerializeField]
    private float _spawnHeight;

    private int _currentRoundCount;

    private GameObject _lastSpawned;
    private Quaternion _spawnRotation;

    public List<GameObject> SpawnedObjects;
    public List<GameObject> DespawnedObjects;

    private void Awake()
    {
        SpawnedObjects = new List<GameObject>();
        DespawnedObjects = new List<GameObject>();
        _spawnRotation = transform.rotation;
        _spawnHeight = transform.position.y;
    }

    public GameObject Spawn()
    {
        if(!EntitySpawnPrefab)
        {
            return null;
        }

        if(DespawnedObjects.Count > 0)
        {
            GameObject ToSpawn = DespawnedObjects[0];
            DespawnedObjects.Remove(ToSpawn);

            _lastSpawned = ToSpawn;
            ToSpawn.GetComponent<CustomerBehavior>().InitOrRefresh();
        }
        else
            _lastSpawned = Instantiate(EntitySpawnPrefab);

        Vector3 pos = transform.position;
        pos.y = _spawnHeight;
        _lastSpawned.transform.position = pos;
        _lastSpawned.transform.rotation = _spawnRotation;

        _lastSpawned.SetActive(true);
        SpawnedObjects.Add(_lastSpawned);

        return _lastSpawned;
    }

    public void SetEntityRotation(Quaternion Rotation)
    {
        _spawnRotation = Rotation;
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
}
