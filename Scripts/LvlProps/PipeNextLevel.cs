using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeNextLevel : MonoBehaviour {

    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            levelManager.LoadLevel("03_Level2");
        }
    }
}
