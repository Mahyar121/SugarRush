using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    [SerializeField] private AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;
    private AudioClip thisLevelMusic;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // Sets the music to the current level variable
        if (scene.buildIndex != 0)
        {
            thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
        }

        // If there is music for the current level
        if (thisLevelMusic)
        {
            // set the audio to play and loop the current  level music
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (scene.buildIndex == 2 || scene.buildIndex == 3)
        {
            audioSource.loop = true;
        }
        else if (scene.buildIndex != 0)
        {
            audioSource.Stop();
        }
    }

    /*
    private void OnLevelWasLoaded(int level)
    {
        // Sets the music to the current level variable
        AudioClip thisLevelMusic = levelMusicChangeArray[level];
        // If there is music for the current level
        if (thisLevelMusic)
        {
            // set the audio to play and loop the current  level music
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (level == 2 || level ==3)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.Stop();
        }
    }
    */
    public void SetVolume(float volume)
    {
        // sets the volume based on the passed in parameter
        audioSource.volume = volume;
    }
}
