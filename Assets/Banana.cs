using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banana : MonoBehaviour {
	public Text scoreText; 
	public float startTime; 
	public string scoreAmount;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col) {
		scoreText.text = (startTime + Time.time).ToString("100");
		Destroy(gameObject);
		}



		//scoreText.text = (startTime ++).ToString("10000");
		//	Destroy(gameObject);
		//Debug.Log(scoreText);
		//Debug.Log("Banana");
	}

