﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	public GameObject audioOnIcon;
	public GameObject audioOffIcon;
	public Text HiScore;
	// Use this for initialization
	void Start () {
		SetSoundState();
		HiScore.text = PlayerPrefs.GetFloat("Hi Score", 0).ToString("0.0");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	public void StartGame(){
		SceneManager.LoadScene("Bunny1 Main");
	}
	public void ToggleSound(){
		if (PlayerPrefs.GetInt("Muted",0)==0)
		{
			PlayerPrefs.SetInt("Muted",1);
		}
		else
		{
			PlayerPrefs.SetInt("Muted",0);	
		}
		SetSoundState();
	}


	private void SetSoundState()
	{
		if (PlayerPrefs.GetInt("Muted", 0)==0)
		{
			AudioListener.volume = 1;
			audioOnIcon.SetActive(true);
			audioOffIcon.SetActive(false);

		}
		else
		{
			AudioListener.volume = 0;
			audioOnIcon.SetActive(false);
			audioOffIcon.SetActive(true);

		}
	} 
	
}
