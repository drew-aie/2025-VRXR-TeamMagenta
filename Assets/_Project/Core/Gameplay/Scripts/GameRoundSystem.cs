using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRoundSystem : MonoBehaviour
{
    private static int _max = 200;
    [SerializeField]
    private List<GameObject> _entitySpawnPrefabs;
    [SerializeField]
    private EntitySpawner _spawner;
    [SerializeField]
    private MeshHealthIndicator _gameHealthIndicator;
    [SerializeField, Min(0)]
    private int _maxRoundCount = 0;
    [SerializeField, Min(0)]
    private int _maxEntitiesActive;
    [SerializeField, Min(1)]
    private int _entitiesPerRound;
    [SerializeField, Range(1, 3)]
    private float _entityRoundMultiplier;
    [SerializeField, Tooltip("The amount of seconds before a new Health point is regenerated for the Player.")]
    private float _secondsBeforeHealthRegen;
    private float _secondsSinceHealthRegen;
    [SerializeField]
    public float _secondsBetweenSceneStartAndRoundStart;
    [SerializeField]
    public float _secondsBetweenRounds;
    private float _secondsSinceLastRound;
    [SerializeField]
    private float _secondsBetweenSpawns;
    private float _secondsSinceLastSpawn;
    private int _currentEntitiesActive;
    private int _currentRoundSpawnCount = 0;

    private int _currentRound;
    public int CurrentRound
    {
        get { return _currentRound;  }
    }
    public UnityEvent OnRoundStart;
    public UnityEvent OnRoundEnd;
    public UnityEvent OnGameEnd;
    public UnityEvent OnGameRestart;

    private void Awake()
    {
        _spawner.SetPrefabs(_entitySpawnPrefabs);

        _currentRound = 1;
        _currentRoundSpawnCount = 0;
        _secondsSinceHealthRegen = 0;
        _currentEntitiesActive = 0;
        //force spawn immediately
        _secondsSinceLastSpawn = _secondsBetweenSpawns;
        //wait period
        _secondsSinceLastRound = _secondsBetweenRounds - _secondsBetweenSceneStartAndRoundStart;
    }

    private void Update()
    {
        bool zeroHealthOrOverMaxRoundCount = _currentRound > _maxRoundCount || _gameHealthIndicator.GetHealthCount() < 1;
        if(zeroHealthOrOverMaxRoundCount)
        {
          OnGameEnd.Invoke();
        }


        bool isTimeToRegen = _secondsSinceHealthRegen > _secondsBeforeHealthRegen;
        if (isTimeToRegen)
        {
            _secondsSinceHealthRegen = 0;
            _gameHealthIndicator.IncreaseHealth();
        }

        _secondsSinceHealthRegen += Time.deltaTime;
        _secondsSinceLastSpawn += Time.deltaTime;
        _secondsSinceLastRound += Time.deltaTime;


        //wait for round begin
        bool RoundHasBegun = _secondsSinceLastRound > _secondsBetweenRounds;
        if (!RoundHasBegun)
            return;
        else
        {
            //go to next wave if max has spawned
            bool nextRoundIfOverMax = _currentRoundSpawnCount >= _entitiesPerRound;
            if (nextRoundIfOverMax)
            {
                //and no entities are currently active.
                if (_currentEntitiesActive < 1)
                {
                    _spawner.DespawnAllEntities();
                    IncrementRound();
                    RestartRound();
                    //scales entities per round by multiplier,
                    //also insures there is an increase.
                    _entitiesPerRound = Mathf.Clamp((int)(_entitiesPerRound * _entityRoundMultiplier), _entitiesPerRound + 1, _max);
                    OnRoundEnd.Invoke();
                    _currentRoundSpawnCount = 0;
                }
            }
            else if (_currentRoundSpawnCount == 0)
                OnRoundStart.Invoke();
        }

        bool underMaxActiveEntities = _currentEntitiesActive < _maxEntitiesActive;
        bool maxSpawnCountIsReached = _currentRoundSpawnCount >= _entitiesPerRound;
        bool spawnWaitPeriodActive = _secondsSinceLastSpawn < _secondsBetweenSpawns;

        //spawns a new customer.
        if (underMaxActiveEntities
        && !maxSpawnCountIsReached
        && !spawnWaitPeriodActive)
        {
             if (_spawner.TriggerCount > 0)
                 return;
             if (!SpawnEntity())
                 return;
           _currentRoundSpawnCount++;
           _currentEntitiesActive++;
        
           _secondsSinceLastSpawn = 0;
        }
    }

    public void RestartRound()
    {
        _spawner.DespawnAllEntities();

        _secondsSinceHealthRegen = 0;
        _secondsSinceLastSpawn = 0;
        _secondsSinceLastRound = 0;
        _currentEntitiesActive = 0;
        _currentRoundSpawnCount = 0;
    }

    public void RestartGame()
    {
        _spawner.DespawnAllEntities();
        _spawner.SetPrefabs(_entitySpawnPrefabs);

        _currentRound = 1;
        _currentRoundSpawnCount = 0;
        _secondsSinceHealthRegen = 0;
        _secondsSinceLastSpawn = 0;
        _secondsSinceLastRound = _secondsBetweenRounds - _secondsBetweenSceneStartAndRoundStart;
        _currentEntitiesActive = 0;

        _gameHealthIndicator.Restart();

        OnGameRestart.Invoke();
    }

    public void IncrementRound()
    {
        _currentRound++;
        //resets wait period before next round.
        _secondsSinceLastRound = 0;
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
