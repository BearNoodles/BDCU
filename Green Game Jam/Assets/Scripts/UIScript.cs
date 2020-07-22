using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScript : MonoBehaviour {

    Text ammoText;
    Text pollutionText;
    Text scoreText;
    Text stoppedText;
    Fish fish;

	// Use this for initialization
	void Start ()
    {
        fish = GameObject.FindGameObjectWithTag("Fish").GetComponent<Fish>();
        ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();	
        pollutionText = GameObject.FindGameObjectWithTag("PollutionText").GetComponent<Text>();	

        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();	
        stoppedText = GameObject.FindGameObjectWithTag("StoppedText").GetComponent<Text>();	

	}
	
	// Update is called once per frame
	void Update ()
    {
        ammoText.text = "Ammo: " + fish.GetCurrentAmmo();
        pollutionText.text = "Pollution Collected: " + fish.GetCurrentPollution();

        scoreText.text = "Cleanup Score: " + fish.GetCurrentScore();
        stoppedText.text = "Polluters Stopped: " + fish.GetCurrentStopped();
    }
}
