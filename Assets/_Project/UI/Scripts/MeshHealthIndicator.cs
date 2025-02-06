using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHealthIndicator : MonoBehaviour
{
    [SerializeField, Tooltip("The Container prefab when health is above 0.")]
    private GameObject _unbroken;
    [SerializeField, Tooltip("The Container prefab when health is below 0.")]
    private GameObject _broken;
    [SerializeField, Tooltip("Empty Health - Last Chance")]
    private GameObject _meshEmpty;
    [SerializeField, Tooltip("Half Health")]
    private GameObject _meshHalfway;
    [SerializeField, Tooltip("Full Health")]
    private GameObject _meshFull;

    private int _healthCount;

    private void Awake()
    {
        _unbroken = Instantiate(_unbroken, transform);
        _broken = Instantiate(_broken, transform);

        _meshEmpty = Instantiate(_meshEmpty, transform);
        _meshHalfway = Instantiate(_meshHalfway, transform);
        _meshFull = Instantiate(_meshFull, transform);

        Restart();
    }

    public void Restart()
    {
        _broken.SetActive(false);
        _unbroken.SetActive(true);

        ChangeMesh(_meshFull);

        _healthCount = 3;
    }

    public void ChangeMesh(GameObject innerContainerMesh)
    {
        DisableMeshes();

        innerContainerMesh.SetActive(true);
    }

    public void DisableMeshes()
    {
        _meshEmpty.SetActive(false);
        _meshHalfway.SetActive(false);
        _meshFull.SetActive(false);
    }

    public void UpdateHealth(int num)
    {
        _healthCount = num;
        switch(_healthCount)
        { 
            default:
                Debug.Log("Invalid health num :" + _healthCount);
                break;
            case 0:
                DisableMeshes();
                _unbroken.SetActive(false);
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

    public void IncreaseHealth()
    {
        _healthCount++;
        UpdateHealth(_healthCount);
    }

    public void DecreaseHealth()
    {
        _healthCount--;
        UpdateHealth(_healthCount);
    }

    public int GetHealthCount()
    {
        return _healthCount;
    }


}
