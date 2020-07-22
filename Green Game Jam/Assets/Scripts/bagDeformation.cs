using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagDeformation : MonoBehaviour {
    float startScaleX;
    float startScaleY;
    float duration = 0.0f;
    float targetScaleX;
    float targetScaleY;
    float lastScaleX;
    float lastScaleY;

    // Use this for initialization
    void Start () {
        startScaleX = transform.localScale.x;
        startScaleY = transform.localScale.y;

        targetScaleX = Random.Range(0.75f, 1.25f) * startScaleX;
        targetScaleY = Random.Range(0.75f, 1.25f) * startScaleY;

        lastScaleX = startScaleX;
        lastScaleY = startScaleY;

    }
	
	// Update is called once per frame
	void Update () {
        duration += Time.deltaTime;
        Vector3 newScale = new Vector3(Mathf.Lerp(lastScaleX, targetScaleX, duration), Mathf.Lerp(lastScaleY, targetScaleY, duration), 1.0f);
        transform.localScale = newScale;
        if (duration > 1.0f){
            duration = 0.0f;
            lastScaleX = transform.localScale.x;
            lastScaleY = transform.localScale.y;
            //targetScaleX = Random.Range(0.75f, 1.25f) * startScaleX;
            targetScaleX = Random.Range(0.75f, 1.0f) * startScaleX;
            //targetScaleY = Random.Range(0.75f, 1.25f) * startScaleY;
            targetScaleY = Random.Range(0.75f, 1.0f) * startScaleY;

        }
	}
}
