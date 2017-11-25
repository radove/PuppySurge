﻿using UnityEngine;
using System.Collections;

public class alienBoss : MonoBehaviour {
	scoreController score;
	public int speed;
	public GameObject bullet;
	public GameObject alienSummon;
	private float randomPositionYStar;
	private float randomPositionXStar;
	private Vector2 destination;
	// Use this for initialization
	void Start () {
		
		//InvokeRepeating ("launchBullet", 1f, 1f);
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			score = gameControllerObject.GetComponent <scoreController>();
		}
		if (score == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		
		randomPositionYStar = Random.Range (-300, 300);
		randomPositionXStar = Random.Range (-500, 1200);
	}
	
	void launchBullet() {
		foreach (Transform child in transform) {
			if (child.gameObject.name.Equals ("bombDropLocation")) {
				bullet.transform.localScale = new Vector3(1f,1f,1f);
				Instantiate(bullet, child.position, Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frames
	void Update () {
		
		if(Vector3.Distance(transform.position, destination) < 0.1f) {
			if (this.transform.position.x > 1100) {
				randomPositionXStar = Random.Range (-400, -500);
			}
			else {
				randomPositionXStar = Random.Range (1100, 1200);
			}
			randomPositionYStar = Random.Range (-300, 300);
		}
		destination = new Vector2 (randomPositionXStar, randomPositionYStar);
		this.transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.deltaTime);
		
	}
}
