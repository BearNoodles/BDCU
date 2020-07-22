using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    Text ammoText;
    Fish fish;

    // Use this for initialization
    void Start()
    {
        fish = GameObject.FindGameObjectWithTag("Fish").GetComponent<Fish>();
        ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: " + fish.GetCurrentAmmo();
        Debug.Log(fish.GetCurrentAmmo());
    }
}