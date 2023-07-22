using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageScript : MonoBehaviour
{
    public AudioSource villageAudio, outsideVillageAudio;
    // Start is called before the first frame update
    void Start()
    {
        villageAudio.Play();
        outsideVillageAudio.Play();
        villageAudio.volume = 0;
        outsideVillageAudio.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the collider, stop playing the exit sound (if it was playing)
            StartCoroutine(FadeOutTransitionAudio(outsideVillageAudio));
            StartCoroutine(FadeInTransitionAudio(villageAudio));
            Debug.Log("Masuk village");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the collider, play the exit sound
            StartCoroutine(FadeOutTransitionAudio(villageAudio));
            StartCoroutine(FadeInTransitionAudio(outsideVillageAudio));
            Debug.Log("Keluar village");
        }
    }
    IEnumerator FadeInTransitionAudio(AudioSource villageAudio)
    {
        villageAudio.Play();
        float targetVolume = 1;
        float fadeTime = 1;

        while (villageAudio.volume < targetVolume)
        {
            villageAudio.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    IEnumerator FadeOutTransitionAudio(AudioSource villageAudio)
    {
        outsideVillageAudio.Play();
        float targetVolume = 0;
        float fadeTime = 1;

        while (villageAudio.volume > targetVolume)
        {
            villageAudio.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
