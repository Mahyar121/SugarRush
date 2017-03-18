using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsLoader : MonoBehaviour {

    private GameObject[] controlObjects;
	// Use this for initialization
	private void Start () {

        controlObjects = GameObject.FindGameObjectsWithTag("ShowControls");
        HideControls();
	}


    public void ShowControls()
    {
        foreach (GameObject objects in controlObjects)
        {
            objects.SetActive(true);
        }
    }

    private void HideControls()
    {
        foreach (GameObject objects in controlObjects)
        {
            objects.SetActive(false);
        }
    }

	
	
}
