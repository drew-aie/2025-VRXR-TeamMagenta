using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))] 
public class RoundUISystem : MonoBehaviour
{

    [SerializeField]
    private GameRoundSystem _roundSystem;

    private TMP_Text _text ;
    private void Awake()
    {
       _text = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if(_text)
        _text.text = _roundSystem.CurrentRound.ToString();
    }
    
}
