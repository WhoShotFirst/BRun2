using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class CoonController : MonoBehaviour {
	private Rigidbody2D coon;
	public float coonJumpForce = 550f; 
	private Animator myAnim;
	private Collider2D myCollider;
	public GameObject Enemy;
	private float coonHurtTime = -1;
	public Text scoreText;
	public float startTime;
	private int jumpsLeft = 2; 
	public AudioSource deathSfx; 
	public AudioSource jumpSfx;


	// Use this for initialization

	void Start () {
		//GetComponentsInChildren( typeof(Transform), true );
		coon = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		myCollider = GetComponent<Collider2D>();

		startTime = Time.time;
	}
	


	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{

			SceneManager.LoadScene("Title");
		}

		if (coonHurtTime == -1) 

		{
			if((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1")) && jumpsLeft > 0)

			{

				if(coon.velocity.y < 0)

				{
					coon.velocity = Vector2.zero; 
				}

				if (jumpsLeft == 1){
					coon.AddForce(transform.up * coonJumpForce *0.75f);
				}
				else
				{
					coon.AddForce(transform.up * coonJumpForce);
				}
				jumpsLeft --; 
				jumpSfx.Play();

			}


			//add Mathf.Abs if you don't want animation to start before character hits ground N.B remeber to put rigibod in double());//
			myAnim.SetFloat("vVelocity",coon.velocity.y); // jump code
			scoreText.text = (Time.time - startTime).ToString("0.0");
		}
		else 
		{
			if (Time.time > coonHurtTime + 2) //time before scene restarts 
				SceneManager.LoadScene("Title"); //load scene on death
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.gameObject.layer ==LayerMask.NameToLayer("Banana"))
		
		{
			startTime += -10;
			Destroy(collision.gameObject);
		}


		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			foreach(MoveLeft movelefter in FindObjectsOfType<MoveLeft>())
			{
				movelefter.enabled = false; //disables enemy movement
			}
			foreach(PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
			{
				spawner.enabled = false; //kills spawner
			}

			coonHurtTime = Time.time;
			myAnim.SetBool("coonHurt", true);
			coon.velocity = Vector2.zero;
			coon.AddForce(transform.up * coonJumpForce);
			myCollider.enabled = false;
			deathSfx.Play();


			float currentHiScore = PlayerPrefs.GetFloat("Hi Score", 0);
			float currentScore = Time.time - startTime;

			if (currentScore > currentHiScore)
			{
				PlayerPrefs.SetFloat ("Hi Score" ,currentScore);
			}
		}
		else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			jumpsLeft = 2;
		}

	}

}﻿