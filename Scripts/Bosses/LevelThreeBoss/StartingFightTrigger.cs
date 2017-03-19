using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingFightTrigger : MonoBehaviour {

    private GameObject firstFlag;
    private GameObject closingWall;
    private GameObject[] bossUI;

    private void Start()
    {
        Setup();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            LevelThreeBoss.Instance.MyAnimator.SetTrigger("GroundPatrol");
            firstFlag.SetActive(true);
            closingWall.SetActive(true);
            foreach(GameObject ui in bossUI)
            {
                ui.SetActive(true);
            }
        }
    }

    private void Setup()
    {
        bossUI = GameObject.FindGameObjectsWithTag("FinalBossUI");
        foreach (GameObject ui in bossUI)
        {
            ui.SetActive(false);
        }
        closingWall = GameObject.Find("VerticalWallCloser");
        closingWall.SetActive(false);
        firstFlag = GameObject.Find("BossFlagOne");
        firstFlag.SetActive(false);
    }
}
