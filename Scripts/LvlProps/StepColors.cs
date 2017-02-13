using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepColors : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color color;
    private float Amount = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            color = RandomizeStepColor();
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Amount = 1f;
            StartCoroutine(Delay());
            
        }
    }

    private IEnumerator Delay()
    {
        for(int i = 0; i < 5; i++)
        {
            Amount -= .2f;
            color = Color.Lerp(Color.white, color, Mathf.Clamp(Amount - 0f / 1f - 0f, 0.0f, 1.0f));
            spriteRenderer.color = color;
            yield return new WaitForSeconds(1f);
        }
        
    }

    private Color RandomizeStepColor()
    {
        Color tempColor;
        int rNumber = Random.Range(0, 255);
        int gNumber = Random.Range(0, 255);
        int bNumber = Random.Range(0, 255);

        tempColor = new Color32((byte)rNumber, (byte)gNumber, (byte)bNumber, 255);
        return tempColor;
    }
}
