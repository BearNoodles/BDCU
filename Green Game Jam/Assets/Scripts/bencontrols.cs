using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bencontrols : MonoBehaviour {

    Rigidbody2D rb;
    float scale = 1.0f;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.up = (Vector2)cursorPos - (Vector2)transform.position;

        Vector2 mouseDirection = (Vector2)cursorPos - (Vector2)this.transform.position;

        rb.velocity = mouseDirection * scale;

        
	}
}
