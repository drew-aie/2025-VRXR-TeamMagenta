using HurricaneVR.Framework.Weapons.Bow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{

    private List<GameObject> _entitySpawnPrefabs;

    private GameObject _lastSpawned;
    private Vector3 _spawnPosition;

    [SerializeField]
    private AnimationClip _enragedClip;
    [SerializeField]
    private AnimationClip _satisfiedClip;
    [HideInInspector]
    public List<GameObject> SpawnedObjects;
    [HideInInspector]
    public List<GameObject> DespawnedObjects;
    [SerializeField]
    private List<SpawnerSpot> _spawnSpots;
    [HideInInspector]
    public int TriggerCount;

    private int _lastSpawnSpotIndex = -1;

    public UnityEvent OnDespawn;

    bool initialized;

    private void Awake()
    {
        SpawnedObjects = new List<GameObject>();
        DespawnedObjects = new List<GameObject>();

        initialized = false;
    }

    public void SetPrefabs(List<GameObject> prefabList)
    {
        _entitySpawnPrefabs = prefabList;
    }

    public GameObject Spawn()
    {
        if(_spawnSpots.Count < 1)
            return null;

        int randomSpawnerSpotIndex = 0;
        bool freshSpawnerSpot = false;
        while (!freshSpawnerSpot)
        {
            randomSpawnerSpotIndex = Random.Range(0, _spawnSpots.Count);
            freshSpawnerSpot = randomSpawnerSpotIndex != _lastSpawnSpotIndex;
        }
        //only used in while loop ^
        _lastSpawnSpotIndex = randomSpawnerSpotIndex;

        _spawnPosition = _spawnSpots[randomSpawnerSpotIndex].transform.position;
        Quaternion rot = _spawnSpots[randomSpawnerSpotIndex].transform.rotation;

        if (!initialized)
        {
            foreach (GameObject obj in _entitySpawnPrefabs)
            {
                _lastSpawned = Instantiate(obj, _spawnPosition, rot);
                _lastSpawned.tag = "Customer";
                DespawnedObjects.Add(_lastSpawned);
                _lastSpawned.SetActive(false);

                obj.GetComponent<CustomerBehavior>().Origin = _spawnPosition;
                Debug.Log(randomSpawnerSpotIndex + " out of " + _spawnSpots.Count);
                randomSpawnerSpotIndex = Random.Range(0, _spawnSpots.Count);
                _spawnPosition = _spawnSpots[randomSpawnerSpotIndex].transform.position;
            }
            initialized = true;
        }

        if (_entitySpawnPrefabs.Count < 1)
            return null;


        if (DespawnedObjects != null
            && DespawnedObjects.Count > 0)
        {
            GameObject ToSpawn = DespawnedObjects[0];
            DespawnedObjects.Remove(ToSpawn);
            _lastSpawned = ToSpawn;
            _lastSpawned.SetActive(true);

            transform.position = _spawnPosition;
            transform.rotation = rot;

            NavMeshAgent navBehavior = _lastSpawned.GetComponent<NavMeshAgent>();
            if (navBehavior)
                navBehavior.enabled = true;
        }
        else
        {
            int randomCustomerPrefab = Random.Range(0, _entitySpawnPrefabs.Count);
            _lastSpawned = Instantiate(_entitySpawnPrefabs[randomCustomerPrefab], _spawnPosition, rot);
            _lastSpawned.tag = "Customer";
        }

        CustomerBehavior behavior = _lastSpawned.GetComponent<CustomerBehavior>();
        behavior.Paths = _spawnSpots[randomSpawnerSpotIndex].Paths;
        behavior.InitOrRefresh();
        behavior.Origin = _spawnPosition;
        
        SpawnedObjects.Add(_lastSpawned);

        return _lastSpawned;
    }

    public bool DespawnEnragedEntity(GameObject obj)
    {
        if (SpawnedObjects.Contains(obj))
        {
            obj.GetComponent<DistanceAnimatorController>().ChangeState(DistanceAnimatorController.MoodState.Enraged);
            float duration = _enragedClip ? _enragedClip.length : 0;
            //adds to despawn list after
            StartCoroutine(Despawn(obj, duration));
            return true;
        }
        else return false;
    }

    public bool DespawnSatisfiedEntity(GameObject obj)
    {
        if (SpawnedObjects.Contains(obj))
        {
            obj.GetComponent<DistanceAnimatorController>().ChangeState(DistanceAnimatorController.MoodState.Satisfied);
            float duration = _satisfiedClip ? _satisfiedClip.length : 0;
            //adds to despawn list after
            StartCoroutine(Despawn(obj, duration));
            return true;
        }
        else return false;
    }

    private IEnumerator Despawn(GameObject obj, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        CustomerBehavior behavior = obj.GetComponent<CustomerBehavior>();
        if (behavior)
            obj.gameObject.transform.position = behavior.Origin;

        SpawnedObjects.Remove(obj);

        obj.SetActive(false);
        DespawnedObjects.Add(obj);

        OnDespawn.Invoke();
    }

    public void DespawnAllEntities()
    {
        for(int i = 0; i < SpawnedObjects.Count; i++)
        {
            GameObject obj = SpawnedObjects[i];
            DespawnEnragedEntity(obj);
        }

        DespawnedObjects.RemoveRange(0, DespawnedObjects.Count);
        SpawnedObjects.RemoveRange(0, SpawnedObjects.Count);
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
