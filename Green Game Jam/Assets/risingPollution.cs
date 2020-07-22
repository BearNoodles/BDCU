using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risingPollution : MonoBehaviour {

    GameController gameController;
    float startY;

    // Use this for initialization
    void Start () {
        gameController = FindObjectOfType<GameController>();
        startY = gameObject.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(0.0f, startY + gameController.pollutionDecomposedCount, 1.0f);

	}
}
