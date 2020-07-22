using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowFade : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    float fadeTime = 1.0f;
    float duration = 0.0f;
    float fadeDirection = -1.0f;
    float fadeAmount = 1.0f;
    Color color;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = GetComponent<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {
        duration += Time.deltaTime * fadeDirection;
        if (duration > fadeTime)
        {
            duration = fadeTime;
            // swap direction in here?
            fadeDirection *= -1.0f;
            fadeAmount = duration / fadeTime;
        } else if (duration < 0.0f)
        {
            duration = 0.0f;
            // swap direction in here?
            fadeDirection *= -1.0f;
            fadeAmount = duration / fadeTime;
        } else
        {
            fadeAmount = duration / fadeTime;
        }
        Color tempColor = GetComponent<SpriteRenderer>().color;
        tempColor.a = Mathf.Lerp(0.0f, 1.0f, fadeAmount);
        spriteRenderer.color = tempColor;
        //Debug.Log(fadeAmount);
	}
}
