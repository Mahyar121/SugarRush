using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlock : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color gray = new Color32(54, 54, 54, 255); // gray
    private Color green = new Color32(0, 255, 0, 255); // green
    private Color red = new Color32(255, 0, 0, 255); // red
    private float time = 0f;
    private bool isGray = false;
    private bool isGreen = false;
    private bool isRed = false;


    // Use this for initialization
    private void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	private void Update () {

        if (Time.time >  time + 1)
        {
            time = Time.time;
            spriteRenderer.color = RandomizePowerColor();
        }    
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && isGreen)
        {
            Player.Instance.OnGreenBlock = true;
            Player.Instance.OnRedBlock = false;
        }
        if (collider.tag == "Player" && isRed)
        {
            Player.Instance.OnGreenBlock = false;
            Player.Instance.OnRedBlock = true;
        }
        if (collider.tag == "Player" && isGray)
        {
            Player.Instance.OnGreenBlock = false;
            Player.Instance.OnRedBlock = false;
        }
    }

    private Color RandomizePowerColor()
    {
        Color tempColor;

        int number = Random.Range(1, 4);

        switch (number)
        {
            case 1:
                tempColor = gray;
                isGray = true;
                isGreen = false;
                isRed = false;
                break;
            case 2:
                tempColor = green;
                isGray = false;
                isGreen = true;
                isRed = false;
                break;
            case 3:
                tempColor = red;
                isGray = false;
                isGreen = false;
                isRed = true;
                break;
            default:
                tempColor = new Color32(255, 255, 255, 255);
                break;
        }
     
        return tempColor;
    }
}
