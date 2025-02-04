using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerBehavior : MonoBehaviour
{
    [SerializeField]
    public float RotationSpeed;

    [SerializeField]
    public float CustomerPathSnapRadius;

    [SerializeField, Range(0.90f, 1)]
    public float SnapRotationAtPercentComplete;
    //path is required
    [HideInInspector]
    public List<LanePath> Paths;

    private Transform _origin;
    private bool _directionChanged;
    private int _pathIndex;
    private NavMeshAgent _navBehavior;
    private void Awake()
    {
       _navBehavior = GetComponent<NavMeshAgent>();
       _origin = transform;
    }
    public void InitOrRefresh()
    {
        if (Paths == null) return;
        if(Paths.Count < 1) return;

        _pathIndex = Random.Range(0, Paths.Count - 1);

        _directionChanged = false;

        transform.position = _origin.position;
    }

    public void FixedUpdate()
    {
       if (Paths.Count < 1)
           return;

        Vector3 target;
        float totalDistance = (Paths[_pathIndex].Start - _origin.position).magnitude;

        bool nearPosition = _navBehavior.remainingDistance < CustomerPathSnapRadius + _navBehavior.speed;
        bool condition = nearPosition && !_directionChanged ? true : false;
        if (condition)
        {
            target = Paths[_pathIndex].End;
            _directionChanged = true;
        }
        else if (!_directionChanged)
        {
            target = Paths[_pathIndex].Start;
        }
        else
            target = Paths[_pathIndex].End;

        //height
       target.y = _navBehavior.baseOffset;
       _navBehavior.SetDestination(target);
       Quaternion desiredRotation = Quaternion.identity;
       Vector3 direction = Paths[_pathIndex].Direction;
        if (_directionChanged)
            desiredRotation.SetLookRotation(Paths[_pathIndex].Direction);
        else
            desiredRotation.SetLookRotation((Paths[_pathIndex].Start - _origin.position).normalized);

       if (Quaternion.Dot(transform.rotation, desiredRotation) > SnapRotationAtPercentComplete)
       {
           transform.rotation = desiredRotation;
       }
       else
       {
           bool rotateRight = Vector2.Dot(transform.right, direction) > 0 ? true : false;

           Vector3 newRotation = transform.rotation.eulerAngles;

           float deltaRotation = RotationSpeed * Time.fixedDeltaTime;
           if (!rotateRight)
               deltaRotation = -deltaRotation;

           newRotation += new Vector3(0, -deltaRotation, 0);

           //obey euler bounds
           if (newRotation.y > 360)
               newRotation.y -= 360;
           if (newRotation.y < 0)
               newRotation.y += 360;

           transform.rotation = Quaternion.Euler(newRotation);
       }
    }
}  
  