using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    private float time;
    private int minute;
    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        time += Time.deltaTime;
        int seconds = (int)(time);
        int milliseconds = Mathf.RoundToInt(time * 1000);


        text.text = "" + seconds / 60 + ":" + seconds % 60 + "." + milliseconds % 1000 / 100;
	}


   
}
