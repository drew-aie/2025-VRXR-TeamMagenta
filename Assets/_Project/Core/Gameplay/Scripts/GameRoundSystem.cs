using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRoundSystem : MonoBehaviour
{
    [SerializeField]
    GameObject EntitySpawnPrefab;
    [SerializeField]
    private EntitySpawner _spawner;
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
    private int _totalRoundSpawnCount = 0;

    private int _currentRoundCount;
    public UnityEvent OnRoundStart;
    public UnityEvent OnRoundEnd;
    public UnityEvent OnGameEnd;
    public UnityEvent OnGameRestart;

    private void FixedUpdate()
    {
        if(_currentRoundCount > _maxRoundCount)
        {
          OnGameEnd.Invoke();
        }
       _secondsSinceLastSpawn += Time.fixedDeltaTime;

       //go to next wave if max has spawned
       if(_totalRoundSpawnCount > _entitiesPerWave)
       {
          _spawner.DespawnAllEntities();
          _currentRoundCount++;
          _entitiesPerWave = (int)(_entitiesPerWave * _entityRoundMultiplier);
          OnRoundEnd.Invoke();
          _totalRoundSpawnCount = 0;
       }
       else if (_totalRoundSpawnCount == 0)
            OnRoundStart.Invoke();

       //spawns a new customer.
       if(_currentEntitiesActive < _maxEntitiesActive 
       && _secondsSinceLastSpawn >= _secondsBetweenSpawns)
       {
            if (_spawner.TriggerCount > 0)
                return;
            if (!SpawnEntity())
                return;
          _totalRoundSpawnCount++;
          _currentEntitiesActive++;

          _secondsSinceLastSpawn = 0;
       }
    }

    public void RestartGame()
    {
        _spawner.DespawnAllEntities();

        _secondsSinceLastSpawn = _secondsBetweenRounds;
        _spawner.EntitySpawnPrefab = EntitySpawnPrefab;

        OnGameRestart.Invoke();
    }

    public void DecrementActiveEntites()
    {
        _currentEntitiesActive--;
    }

    public GameObject SpawnEntity()
    {
        return _spawner.Spawn();
    }

    public bool DespawnEnragedEntity(GameObject obj)
    {
        //waits until animation clip is complete.
        _spawner.DespawnEnragedEntity(obj);
        
        return true;
    }
}
