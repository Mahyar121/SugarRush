using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour {

    [SerializeField] private Spike spike;

    private Rigidbody2D myrigidbody;

	// Use this for initialization
	void Start ()
    {
        myrigidbody = spike.GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            myrigidbody.simulated = true;
        }
    }
}
