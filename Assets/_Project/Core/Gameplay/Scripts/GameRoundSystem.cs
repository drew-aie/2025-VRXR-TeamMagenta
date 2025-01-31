using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRoundSystem : MonoBehaviour
{
    [SerializeField]
    GameObject EntitySpawnPrefab;
    public EntitySpawner _spawner;
    [SerializeField]
    private List<LanePath> _paths;
    [SerializeField, Min(0)]
    private int _maxRoundCount = 0;
    [SerializeField, Min(0)]
    private int _maxEntitiesActive;
    [SerializeField, Min(1)]
    private int _entitiesPerWave;
    [SerializeField, Range(1, 3)]
    private float _entityRoundMultiplier;
    [SerializeField]
    public float _secondsBetweenRounds;
    [SerializeField]
    private float _secondsBetweenSpawns;
    private float _secondsSinceLastSpawn;
    private int _currentEntitiesActive;
    private int _totalRoundSpawnCount;

    private int _currentRoundCount;
    public UnityEvent OnEntitySpawn;
    public UnityEvent OnEntityDespawn;
    public UnityEvent OnRoundStart;
    public UnityEvent OnRoundEnd;
    public UnityEvent GameEnd;


    private void FixedUpdate()
    {
        if(_currentRoundCount > _maxRoundCount)
        {
          GameEnd.Invoke();
        }
       _secondsSinceLastSpawn += Time.fixedDeltaTime;

       //if spawned more than max
       if(_totalRoundSpawnCount > _maxEntitiesActive)
       {
          _spawner.DespawnAllEntities();
          _currentRoundCount++;
          _entitiesPerWave = (int)(_entitiesPerWave * _entityRoundMultiplier);
          OnRoundEnd.Invoke();
       }
       else if (_currentEntitiesActive == 0)
       OnRoundStart.Invoke();

       //spawns a new customer.
       if(_currentEntitiesActive < _maxEntitiesActive 
       && _secondsSinceLastSpawn >= _secondsBetweenSpawns)
       {
          int randomIndex = Random.Range(0, _paths.Count);

          LanePath randomPath = _paths[randomIndex];

          SpawnEntity().GetComponent<CustomerBehavior>().ChosenPath = randomPath;

          OnEntitySpawn.Invoke();

          _secondsSinceLastSpawn -= _secondsBetweenSpawns;
          _currentEntitiesActive++;
       }
    }

    public void RestartGame()
    {
        _spawner.DespawnAllEntities();

        _secondsSinceLastSpawn = _secondsBetweenRounds;
        _spawner.EntitySpawnPrefab = EntitySpawnPrefab;
    }

    public void DecrementActiveEntites()
    {
        _currentEntitiesActive--;
    }

    public GameObject SpawnEntity()
    {
        return _spawner.Spawn();
    }

    public bool DespawnEntity(GameObject obj)
    {
        _spawner.DespawnOwnedEntity(obj);
        
        OnEntityDespawn.Invoke();
        return true;
    }
}
