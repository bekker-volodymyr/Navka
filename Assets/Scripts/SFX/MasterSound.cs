using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterSound : MonoBehaviour
{
    [SerializeField]private Slider volumeSlider;

    // Reference to the AudioSource component
    [SerializeField]private List<AudioSource> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioSource source in audioSources)
        {
            // Set the initial volume
            source.volume = volumeSlider.value;

            // Add a listener to call the OnVolumeChange method when the slider value changes
            volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        }
    }

    // This method will be called when the slider value changes
    void OnVolumeChange(float value)
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = value;
        }
    }
}
