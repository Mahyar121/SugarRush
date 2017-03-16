using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour {

    [SerializeField] private Text Up, Down, Left, Right, Attack, Jump, Action, Menu;
	// Use this for initialization
	void Start () {

        Up.text = "Up Arrow";
        Down.text = "Down Arrow";
        Left.text = "Left Arrow";
        Right.text = "Right Arrow";
        Attack.text = "Z";
        Jump.text = "SpaceBar";
        Action.text = "X";
        Menu.text = "P";
	}
	
}
