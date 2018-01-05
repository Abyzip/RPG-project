using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAreaTribeTrigger : MonoBehaviour {
    public AudioClip otherClip;
    public AudioSource audio;
    public AudioClip startClip;
    public float FadeTime= 0.5f;
    public bool canChangeMusic = true;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            float startVolume = audio.volume;
           


            StartCoroutine("FadeOutEnter");
         
        }
  
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player" )
        {
            float startVolume = audio.volume;
            

            StartCoroutine("FadeOutExit");

        }

    }

    IEnumerator FadeOutEnter()
    {
        while (!canChangeMusic)
        {
            yield return null;
        }
        canChangeMusic = false;
        float startVolume = audio.volume;

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audio.Stop();
        //audio.volume = startVolume;
        startClip = audio.clip;
        audio.clip = otherClip;
        audio.Play();
        while (audio.volume < startVolume)
        {
            audio.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        canChangeMusic = true;
    }

    IEnumerator FadeOutExit()
    {
        while (!canChangeMusic)
        {
            yield return null;
        }
        canChangeMusic = false;
        float startVolume = audio.volume;

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audio.Stop();
        //audio.volume = startVolume;
        audio.clip = startClip;
        audio.Play();
        while (audio.volume < startVolume)
        {
            audio.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        canChangeMusic = true;
    }


}
