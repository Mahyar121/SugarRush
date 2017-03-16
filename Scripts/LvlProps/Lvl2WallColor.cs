using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2WallColor : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private float time = 0f;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > time + 0.6f)
        {
            time = Time.time;
            spriteRenderer.color = RandomizeWallColor();
        }
    }


    private Color RandomizeWallColor()
    {
        Color tempColor;
        int rNumber = Random.Range(0, 255);
        int gNumber = Random.Range(0, 255);
        int bNumber = Random.Range(0, 255);

        tempColor = new Color32((byte)rNumber, (byte)gNumber, (byte)bNumber, 255);
        return tempColor;
    }
}
