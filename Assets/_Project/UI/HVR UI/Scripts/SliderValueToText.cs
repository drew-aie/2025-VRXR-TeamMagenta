using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueToText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _sliderValueText;

    [SerializeField]
    private Slider _slider;

    public void UpdateValue()
    {
        _sliderValueText.text = _slider.value + "";
    }
}
