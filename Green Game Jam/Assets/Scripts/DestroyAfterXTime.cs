﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXTime : MonoBehaviour {
    public float destroyTime = 1.6f;


	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
