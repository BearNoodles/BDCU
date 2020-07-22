using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour {

    public Vector3 velocity;

	// Use this for initialization
	void Start ()
    {
        velocity = new Vector3(-0, 0, 0);	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += velocity * Time.deltaTime;	
	}
}
