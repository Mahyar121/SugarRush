using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThreeFlagController : MonoBehaviour {

    [SerializeField] private GameObject[] flags;

    private void Start()
    {
        FlagInitialization();
    }
    
    private void FlagInitialization()
    {
        foreach (GameObject flag in flags)
        {
            flag.SetActive(false);
        }
    }

    private void Update()
    {
        if (flags[0] == null && flags[1] != null)
        {
            LevelThreeBoss.Instance.MyAnimator.SetTrigger("FirstAirPatrol");
        }
        else if (flags[0] == null && flags[1] == null && flags[2] != null)
        {
            LevelThreeBoss.Instance.MyAnimator.SetTrigger("SecondAirPatrol");
        }
        else if (flags[0] == null && flags[1] == null && flags[2] == null)
        {
            LevelThreeBoss.Instance.MyAnimator.SetTrigger("SecondGroundPatrol");
        }
    }
}
