using HurricaneVR.Framework.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(TMP_Text))]
public class MenuBehavior : MonoBehaviour
{
    [SerializeField]
    GameRoundSystem _roundSystem;

    private TMP_Text _buttonText;
    private void Awake()
    {
        _buttonText = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if()
    }

    private void OnEnable()
    {
        if(_buttonText)
        {

        }
    }

   // private IEnumerator 
  //  {
        
   // }



}
