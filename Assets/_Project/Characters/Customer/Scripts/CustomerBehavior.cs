using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(DistanceAnimatorController))]
[RequireComponent(typeof(Animator))]
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

    [HideInInspector]
    public Vector3 Origin;

    private Vector3 _target;
    private bool _directionChanged;
    private int _pathIndex;
    private NavMeshAgent _navBehavior;
    private void Awake()
    {
       _navBehavior = GetComponent<NavMeshAgent>();
    }
    public void InitOrRefresh()
    {
        if (Paths == null) return;
        if(Paths.Count < 1) return;

        _pathIndex = Random.Range(0, Paths.Count);
        GetComponent<DistanceAnimatorController>().SetPath(Paths[_pathIndex]);

        _target = Paths[_pathIndex].Start;
        _directionChanged = false;
    }

    public void FixedUpdate()
    {
       if (Paths.Count < 1)
           return;

       bool nearPosition = _navBehavior.remainingDistance < _navBehavior.speed + CustomerPathSnapRadius;
       if (nearPosition && !_directionChanged)
       {
           _target = Paths[_pathIndex].End;
           _directionChanged = true;
       }
       _navBehavior.SetDestination(_target);

       Quaternion desiredRotation = Quaternion.identity;
       desiredRotation.SetLookRotation((_target - transform.position).normalized); 

       Vector3 direction = Paths[_pathIndex].Direction;
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
  