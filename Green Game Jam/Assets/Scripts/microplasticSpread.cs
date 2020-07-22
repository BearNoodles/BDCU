using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class microplasticSpread : MonoBehaviour {
    float spreadTime = 1.0f;
    float duration = 0.0f;
    SpriteRenderer childSpriteRenderer;
    float fadeAmount = 0.0f;

    // Use this for initialization
    void Start () {
        childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        duration += Time.deltaTime;
        if (duration > spreadTime)
        {
            Destroy(gameObject);
        }
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * (1.0f + duration);

        fadeAmount = duration / spreadTime;

        Color tempColor = childSpriteRenderer.color;
        tempColor.a = Mathf.Lerp(1.0f, 0.0f, fadeAmount);
        childSpriteRenderer.color = tempColor;
    }
}
