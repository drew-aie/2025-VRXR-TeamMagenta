using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHealthIndicator : MonoBehaviour
{
    [SerializeField, Tooltip("The Container prefab when health is below 0.")]
    private GameObject _broken;
    [SerializeField, Tooltip("Empty Health - Last Chance")]
    private GameObject _meshEmpty;
    [SerializeField, Tooltip("Half Health")]
    private GameObject _meshHalfway;
    [SerializeField, Tooltip("Full Health")]
    private GameObject _meshFull;

    private int _healthCountMax = 3;
    private int _healthCount;
    public int Health { get => _healthCount; }
    private void Awake()
    {
        _broken = Instantiate(_broken, transform);

        _meshEmpty = Instantiate(_meshEmpty, transform);
        _meshHalfway = Instantiate(_meshHalfway, transform);
        _meshFull = Instantiate(_meshFull, transform);

        Restart();
    }

    private void ChangeMesh(GameObject innerContainerMesh)
    {
        DisableMeshes();

        if(innerContainerMesh)
            innerContainerMesh.SetActive(true);
    }

    private void DisableMeshes()
    {
        if(_meshEmpty)
            _meshEmpty.SetActive(false);
        if(_meshHalfway)
            _meshHalfway.SetActive(false);
        if(_meshFull)
            _meshFull.SetActive(false);
    }

    private void UpdateHealth(int num)
    {
        if (!_broken)
            return;

        _healthCount = num;
        switch(_healthCount)
        { 
            default:
                Debug.Log("Invalid health num :" + _healthCount);
                break;
            case 0:
                DisableMeshes();
                _broken.SetActive(true);
                break;
            case 1:
                ChangeMesh(_meshEmpty); break;
            case 2:
                ChangeMesh(_meshHalfway); break;
            case 3: 
                ChangeMesh(_meshFull); break;
        }
    }
    public void Restart()
    {
        _broken.SetActive(false);

        ChangeMesh(_meshFull);

        ResetHealth();
    }

    public void ResetHealth()
    {
        _healthCount = _healthCountMax;
    }

    public void IncreaseHealth()
    {
        if(_healthCount >= _healthCountMax)
            return;
        _healthCount++;
        UpdateHealth(_healthCount);
    }

    //called using UnityEvent OnDespawn from EntitySpawner
    public void DecreaseHealth()
    {
        if (_healthCount < 1)
            return;
        _healthCount--;
        UpdateHealth(_healthCount);
    }

    public int GetHealthCount()
    {
        return _healthCount;
    }


}
