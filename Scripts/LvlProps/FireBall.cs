using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    [SerializeField] private float respawnPoint;
    [SerializeField] private float movementSpeed;

    private Vector3 startingPoint;
    
	// Use this for initialization
	private void Start ()
    {
        startingPoint = transform.position;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (transform.position.y <= respawnPoint)
        {
            MoveFireBall();
        }
        else if (transform.position.y >= respawnPoint)
        {
            ResetFireBall();
        }
        
	}

    private void MoveFireBall()
    {
        transform.Translate(0f, movementSpeed * Time.deltaTime, 0f);
    }
    private void ResetFireBall()
    {
        transform.position = startingPoint;
    }
}
