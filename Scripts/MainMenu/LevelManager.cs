using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField] private float autoLoadNextLevel;

    private void Start()
    {
        if(autoLoadNextLevel > 0) { Invoke("LoadNextLevel", autoLoadNextLevel); }
    }


    // different ways to load levels, I chose to load the level by name
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Quit the game
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
