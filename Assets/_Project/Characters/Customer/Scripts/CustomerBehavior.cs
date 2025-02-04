using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomerBehavior : MonoBehaviour
{
    
    [SerializeField]
    public float RotationSpeed;

    [SerializeField]
    public float PathMoveSpeed;

    [SerializeField, Range(1.25f, 5)]
    public float CustomerPathSnapRadius;

    [SerializeField, Range(0.90f, 1)]
    public float SnapRotationAtPercentComplete;

    [HideInInspector]
    public float PathDistanceTravelled;

    //path is required
    [HideInInspector]
    public LanePath ChosenPath;

    private Rigidbody _rigidBody;
    private bool _onPath;
    private bool _pathSet;

    private void Awake()
    {
       _rigidBody = GetComponent<Rigidbody>();
       InitOrRefresh();
    }
    public void InitOrRefresh()
    {
        _onPath = false;
        ChosenPath = null;
        PathDistanceTravelled = 0;
    }
   
    public void FixedUpdate()
    {
        if (!ChosenPath)
            return;
   
     Vector3 direction;
     
     if (_onPath)
         direction = ChosenPath.Direction;
     else
     {
         direction = ChosenPath.Start.transform.position - _rigidBody.position;
         direction.Normalize();
     }
     
     //rotate
     {
         Quaternion desiredRotation = Quaternion.identity;
         if (!_onPath)
         {
             desiredRotation.SetLookRotation((ChosenPath.Start.transform.position - _rigidBody.transform.position).normalized);
     
             if (Quaternion.Dot(transform.rotation, desiredRotation) > SnapRotationAtPercentComplete)
             {
                 _rigidBody.rotation = desiredRotation;
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
     
                 _rigidBody.rotation = Quaternion.Euler(newRotation);
             }
         }
         else
         //hard set rotation
         {
             _rigidBody.rotation = desiredRotation;
         }
      }
       //move
      {
         Vector3 force = direction * PathMoveSpeed * Time.fixedDeltaTime;
         if (!_onPath)
         {
             //arrive at path
             if ((ChosenPath.Start.transform.position - _rigidBody.position).magnitude < CustomerPathSnapRadius)
             {
                 _rigidBody.velocity = Vector3.zero;
                 _rigidBody.MovePosition(ChosenPath.Start.transform.position);
                 _onPath = true;
             }
         }
             _rigidBody.AddForce(force, ForceMode.VelocityChange);
      }
   }
}  
  