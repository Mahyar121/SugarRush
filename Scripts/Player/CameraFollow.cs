using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private float xMin, xMax, yMin, yMax;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
    }
	
	// Update is called once per frame
	private void LateUpdate ()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, xMin, xMax),
            Mathf.Clamp(target.position.y, yMin, yMax),
            transform.position.z
            );
	}
}
