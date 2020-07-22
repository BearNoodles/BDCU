using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int pollutersSleepingWithTheFishes = 0;
    public int pollutionDecomposedCount = 0;
    public float pollutionLifetime = 10.0f;

    public GameObject boatman;
    public float boatmanSpawnDelay = 10.0f;
    public float boatmanHeight = 41.0f;
    float spawnTimer = 0.0f;

    public GameObject diver;
    public float diverSpawnDelay = 15.0f;
    public float diverAvgHeight = 0.0f;
    public float diverHeightVariation = 20.0f;
    float diverSpawnTimer = 0.0f;

    // Use this for initialization
    void Start () {
        Instantiate(boatman, new Vector3(-60.0f, boatmanHeight, 0.0f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(polutionDecomposedCount);
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= boatmanSpawnDelay)
        {
            Instantiate(boatman, new Vector3(-60.0f, boatmanHeight, 0.0f), Quaternion.identity);
            spawnTimer = 0.0f;
        }

        diverSpawnTimer += Time.deltaTime;
        if (diverSpawnTimer >= diverSpawnDelay)
        {
            Instantiate(diver, new Vector3(60.0f, diverAvgHeight + Random.Range(-diverHeightVariation, diverHeightVariation)), Quaternion.identity);
            diverSpawnTimer = 0.0f;
        }
	}
}
