using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Slider))]
public class SliderValueToText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _sliderValueText;

    private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateValue()
    {
        _sliderValueText.text = _slider.value + "";
    }
}
