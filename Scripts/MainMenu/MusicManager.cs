using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    [SerializeField] private AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;

    private void Awake()
    {
        // keeps the musicManager gameobject alive for other scenes when they load
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    private void Start () {
        // GetComponent: searches for a audioSource and grabs it
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }

    private void OnLevelWasLoaded(int level)
    {
        // Sets the music to the current level variable
        AudioClip thisLevelMusic = levelMusicChangeArray[level];
        // If there is music for the current level
        if(thisLevelMusic)
        {
            // set the audio to play and loop the current  level music
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        // sets the volume based on the passed in parameter
        audioSource.volume = volume;
    }
}
