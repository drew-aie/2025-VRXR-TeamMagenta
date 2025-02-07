using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Slider _masterVolumeSlider;

    [SerializeField, Tooltip("Scales Overall Audio Volume"), Range(0, 1)]
    private float _volumeScalar = 1;

    [SerializeField]
    private List<AudioSource> _sources;

    private void Awake()
    {
        foreach (AudioSource source in _sources)
        {
            if(!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    public void UpdateAudioVolume()
    {
        if (_sources.Count < 1)
            return;

        float volume = _masterVolumeSlider.value / 100;
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        volume *= _volumeScalar;
        foreach (AudioSource source in _sources)
            source.volume = volume;
    }
}
