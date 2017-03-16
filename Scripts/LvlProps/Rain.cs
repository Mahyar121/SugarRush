using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    [SerializeField] private float respawnPoint;
    [SerializeField] private float movementSpeed;

    private Vector3 startingPoint;

    // Use this for initialization
    private void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y >= respawnPoint)
        {
            MoveRainDrop();
        }
        else if (transform.position.y <= respawnPoint)
        {
            ResetRainDrop();
        }

    }

    private void MoveRainDrop()
    {
        transform.Translate(0f, -movementSpeed * Time.deltaTime, 0f);
    }
    private void ResetRainDrop()
    {
        transform.position = startingPoint;
    }
}
