using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretStepColor : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color color;
    private float Amount = 1f;
    private bool hasStepped = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !hasStepped)
        {
            color = ChangingSecretStepColor();
            spriteRenderer.color = color;
            hasStepped = true;
            StartCoroutine(Delay());

        }
    }

    private IEnumerator Delay()
    {
        for (int i = 0; i < 5; i++)
        {
            Amount -= .2f;
            color = Color.Lerp(Color.black, color, Mathf.Clamp(Amount - 0f / 1f - 0f, 0.0f, 1.0f));
            spriteRenderer.color = color;
            yield return new WaitForSeconds(.3f);
        }
        if (transform.parent)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private Color ChangingSecretStepColor()
    {
        Color tempColor;
        tempColor = new Color32(145, 0, 0, 255);
        return tempColor;
    }
}
