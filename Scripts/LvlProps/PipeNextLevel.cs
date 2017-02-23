using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeNextLevel : MonoBehaviour {

    [SerializeField] private int skiplevel;

    private int level;

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            SceneManager.LoadScene(level + skiplevel); // skips the level 1 boss and jumps the player to level 2
        }
    }
}
