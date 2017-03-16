using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingFightTrigger : MonoBehaviour {

    private GameObject firstFlag;

    private void Start()
    {
        firstFlag = GameObject.Find("BossFlagOne");
        firstFlag.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            LevelThreeBoss.Instance.MyAnimator.SetTrigger("GroundPatrol");
            firstFlag.SetActive(true);
        }
    }
}
