using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {
	private Rigidbody2D myRigidBody; 
	public float bunnyJumpForce = 500f;
	private Animator myAnim; 
	public GameObject Enemy;
	private float bunnyHurtTime = -1;
	private Collider2D myCollider; 
	public Text scoreText;
	private float startTime;
	private int jumpsLeft = 2; 
	public AudioSource deathSfx; 
	public AudioSource jumpSfx; 



	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		myCollider = GetComponent<Collider2D>();

		startTime = Time.time;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{

			SceneManager.LoadSceneAsync("Title");
		}

		if (bunnyHurtTime == -1) 
		
		{
			if(Input.GetButtonUp("Jump") && jumpsLeft > 0)
				
		{

	   	if(myRigidBody.velocity.y < 0)
				
				{
		myRigidBody.velocity = Vector2.zero; 
				}

				if (jumpsLeft == 1){
			myRigidBody.AddForce(transform.up * bunnyJumpForce *0.75f);
				}
				else
				{
					myRigidBody.AddForce(transform.up * bunnyJumpForce);
				}
				jumpsLeft --; 
				jumpSfx.Play();

		}


		//add Mathf.Abs if you don't want animation to start before character hits ground N.B remeber to put rigibod in double());//
		myAnim.SetFloat("vVelocity",myRigidBody.velocity.y); // jump code
			scoreText.text = (Time.time - startTime).ToString("0.0");
		}
		else 
		{
			if (Time.time > bunnyHurtTime + 2) //time before scene restarts 
				SceneManager.LoadSceneAsync("Title"); //load scene on death
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
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


			bunnyHurtTime = Time.time;
			myAnim.SetBool("bunnyHurt", true);
			myRigidBody.velocity = Vector2.zero;
			myRigidBody.AddForce(transform.up * bunnyJumpForce);
			myCollider.enabled = false;
			deathSfx.Play();
		
		}
		else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			jumpsLeft = 2;
		}
			
		}
		
	}﻿
	
