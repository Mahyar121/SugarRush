using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMobSight : MonoBehaviour {

    [SerializeField] private MeleeMob mob;

    private void OnTriggerEnter2D(Collider2D collider) // if player enters the mob's sight
    {
        if (collider.tag == "Player") { mob.Target = collider.gameObject; }
    }

    private void OnTriggerExit2D(Collider2D collider) // if the player leaves the mob's site
    {
        if (collider.tag == "Player") { mob.Target = null; }
    }

}
