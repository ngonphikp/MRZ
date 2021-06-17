using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    AudioSource audioSource = null;

    private void Awake()
    {
        MakeSingleInstance();

        audioSource = GetComponent<AudioSource>();
    }

    private void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayOneShot(AudioClip audioClip = null, float volume = 1.0f)
    {
        if(audioClip) audioSource.PlayOneShot(audioClip, volume);
    }
}
