using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    [SerializeField] private float destroyPoint;
    private Rigidbody2D myRigidBody;
    private Vector3 spawnLocation;
    private float respawnTime = 5f;
    private bool IsDead = false;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.gravityScale = 3f;
        spawnLocation = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < destroyPoint)
        {
            Destroy(gameObject);
        }   
    }


    

}
