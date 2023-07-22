using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public TMP_Dropdown graphicDropdown;

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;


    // Start is called before the first frame update
    void Start()
    {
        string[] graphicsOptions = QualitySettings.names;
        graphicDropdown.AddOptions(new List<string>(graphicsOptions));

        graphicDropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioMixer audioMixer;

    public void setAudioVolume(float volume)
    {
        audioMixer.SetFloat("audioVolume", volume);
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void OnGraphicsDropdownChanged(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void setFullScreen (bool isFullSCreen)
    {
        Screen.fullScreen = isFullSCreen;
    }
 
}
