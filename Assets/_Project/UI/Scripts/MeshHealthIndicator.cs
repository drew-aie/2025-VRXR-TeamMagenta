using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHealthIndicator : MonoBehaviour
{
    [SerializeField, Tooltip("Displayed during player lose state")]
    private Mesh _negativeHealth;
    [SerializeField, Tooltip("Empty Health - Last Chance")]
    private Mesh _zeroHealth;
    [SerializeField, Tooltip("Half Health")]
    private Mesh _oneHealth;
    [SerializeField, Tooltip("Full Health")]
    private Mesh _twoHealth;

    private MeshFilter _meshFilter;

    public void UpdateHealth(int num)
    {
        switch(num)
        { 
            default:
                Debug.Log("Invalid health num");
                Debug.Break(); break;
            case 0:
                _meshFilter.mesh = _negativeHealth; break;
            case 1:
                _meshFilter.mesh = _zeroHealth; break;
            case 2:
                _meshFilter.mesh = _oneHealth; break;
            case 3: 
                _meshFilter.mesh = _twoHealth; break;
        }
    }


}
