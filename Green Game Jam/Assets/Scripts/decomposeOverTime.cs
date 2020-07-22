using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decomposeOverTime : MonoBehaviour {

    float lifetime = 0.0f;
    float decomposeTime = 10.0f;
    public GameObject microplastics;
    GameController gameController;
    public AK.Wwise.Event disintegrateSound = new AK.Wwise.Event();

    // Use this for initialization
    void Start () {
        gameController = FindObjectOfType<GameController>();
        decomposeTime = gameController.pollutionLifetime;
	}
	
	// Update is called once per frame
	void Update () {
        lifetime += Time.deltaTime;
        if (lifetime >= decomposeTime)
        {
            // spawn microplastics
            Instantiate(microplastics, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            // some kind of scoring thing?
            gameController.pollutionDecomposedCount += 1;
            // sound
            disintegrateSound.Post(gameObject);
            // actually die
            Destroy(gameObject);
        }
	}
}
