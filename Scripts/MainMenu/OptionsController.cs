using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private LevelManager levelManager;

    private MusicManager musicManager;

	// Use this for initialization
	private void Start () {
        // Searches for the musicmanager component
        musicManager = FindObjectOfType<MusicManager>();
        // initializes the volume based on what was saved for the game
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
	}
	
	// Update is called once per frame
	private void Update () {
        musicManager.SetVolume(volumeSlider.value);
	}

    // Back button
    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        levelManager.LoadLevel("01a_MainMenu");
    }

    // Default button
    public void SetDefaults()
    {
        volumeSlider.value = 0.7f;
    }
}
