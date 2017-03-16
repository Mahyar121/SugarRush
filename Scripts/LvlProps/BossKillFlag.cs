using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossKillFlag : MonoBehaviour {

    private CameraFollow camera;
    private Animator bossAnimator;
    private bool isPlayerOn;
    private float time;

    private void Start()
    {
        time = 0f;
        isPlayerOn = false;
        camera = GameObject.FindObjectOfType<CameraFollow>();
        bossAnimator = LevelOneBoss.Instance.GetComponent<Animator>();

    }

    private void Update()
    {
        if (isPlayerOn)
        {
            Delay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            time = Time.time;
            camera.Target = GameObject.FindObjectOfType<LevelOneBoss>().transform;
            LevelOneBoss.Instance.IsPlayerOnFlag = true;
            bossAnimator.SetTrigger("death");
            isPlayerOn = true;
        }
    }

    private void Delay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 8) // LEVEL 1
        {
            if (Time.time > time + 2.3f)
            {
                SceneManager.LoadScene(9); // Loads level 2
            }
        }
        
    }
}
