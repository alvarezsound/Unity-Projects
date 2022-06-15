using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioController;
    private static AudioController _instance;
    public static AudioController instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        instance = _instance;

        DontDestroyOnLoad(this);
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        audioController.volume = volume;
        audioController.PlayOneShot(clip);
    }
}
