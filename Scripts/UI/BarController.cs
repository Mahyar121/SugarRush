using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

    
    [SerializeField] private float lerpSpeed; // the rate the hp bar drops or increases
    [SerializeField] private Image image;
    [SerializeField] private Text text; // Health : 100

    private float fillAmount;

    public float MaxValue { get; set; }
    public float Value
    {
        set
        { 
            string[] tmp = text.text.Split(':'); // breaks up the Health and 100 from Health : 100
            text.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
    
    private void HandleBar()
    {
        if (fillAmount != image.fillAmount)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, fillAmount, Time.deltaTime * lerpSpeed); // if hp changes, make UI display it
        }

        ChangingBarColor();

    }

    private void Update()
    {
        HandleBar();
    }

    // passed in value, 0 as min hp, Max HP, 0 as min, and 1 as max
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }


    private void ChangingBarColor()
    {
        image.color = Color.Lerp(Color.red, Color.green, Mathf.Clamp(fillAmount - 0f / 1f - 0f, 0.0f, 1.0f));
    }
}
