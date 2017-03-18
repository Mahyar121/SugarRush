using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour {

    private GameObject[] controlObjects;

    [SerializeField] private Text Up, Down, Left, Right, Attack, Jump, Action, Menu;
	// Use this for initialization
	private void Start () {

        Up.text = "Up Arrow";
        Down.text = "Down Arrow";
        Left.text = "Left Arrow";
        Right.text = "Right Arrow";
        Attack.text = "Z";
        Jump.text = "SpaceBar";
        Action.text = "X";
        Menu.text = "P";
	}

    public void BackButtonHandler()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            controlObjects = GameObject.FindGameObjectsWithTag("ShowControls");
            foreach (GameObject objects in controlObjects)
            {
                objects.SetActive(false);
            }
        }
    }
	
}
