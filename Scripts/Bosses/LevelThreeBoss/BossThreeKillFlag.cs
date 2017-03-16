using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThreeKillFlag : MonoBehaviour {

    [SerializeField] private GameObject nextFlag;
    [SerializeField] int lastOne;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (lastOne == 0)
            {
                nextFlag.SetActive(true);
                LevelThreeBoss.Instance.HealthStat.CurrentHp -= 16.7f;
                Destroy(gameObject);
            }
            else
            { 
                LevelThreeBoss.Instance.HealthStat.CurrentHp -= 16.7f;
                Destroy(gameObject);
            }
           
        }
    }
}
