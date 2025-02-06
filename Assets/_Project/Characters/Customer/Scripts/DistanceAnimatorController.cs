using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DistanceAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private LanePath _path;
    private float _pathDistance;
    public enum MoodState
    {
        None,
        Satisfied,
        Calm,
        Frustrated,
        Angry,
        Enraged
    }
    private int _moodCount = 5;

    private MoodState _state = MoodState.None;
    private void Awake()
    {
        Refresh();
    }

    private void Refresh()
    {
        _animator = GetComponentInChildren<Animator>();
        _state = MoodState.Calm;
        _path = null;
    }

    private void FixedUpdate()
    {
        float distanceToEnd = (_path.End - transform.position).magnitude;
        //get distance between each mood checkpoint.
        float distanceBetween = _pathDistance / /*subtract finished state*/(_moodCount - 1);
        if(_path)
        switch(_state)
        {
            default:
                break;
            case MoodState.Satisfied:
                //wait for despawn
                break;
            case MoodState.Calm:
                //if AI is past the travelled distance threshold
                if (_pathDistance - distanceToEnd > distanceBetween * 1)
                    ChangeState(MoodState.Frustrated);
                break;
            case MoodState.Frustrated:
                //if AI is past the travelled distance threshold
                if (_pathDistance - distanceToEnd > distanceBetween * 2)
                    ChangeState(MoodState.Angry);
                break;
            case MoodState.Angry:
                //wait for despawn
                    break;
        }
    }

    public void ChangeState(MoodState stateEnum)
    {
        _animator.SetInteger("stateNum", (int)stateEnum);
    }

    public void SetPath(LanePath path)
    {
        _path = path;
    }

}
