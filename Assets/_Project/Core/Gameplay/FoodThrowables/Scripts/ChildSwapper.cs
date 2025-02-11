using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;


public class ChildSwapper : MonoBehaviour
{
    public List<GameObject> _childrenPrefabs;
    private void Awake()
    {
        int randomNum = Random.Range(0, _childrenPrefabs.Count);
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

        meshFilter.mesh = _childrenPrefabs[randomNum].GetComponentInChildren<MeshFilter>().sharedMesh;
        meshRenderer.material = _childrenPrefabs[randomNum].GetComponentInChildren<MeshRenderer>().sharedMaterial;

        GameObject child = meshRenderer.gameObject;

        child.transform.localScale = transform.localScale;
    }
}
