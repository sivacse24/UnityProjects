﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Finding the stage size
 * Finding the object size
 * MovePlayer only inside the screen size 
*/

public class MovePlayerInStage : MonoBehaviour {

	private float moveX;
	private float moveY;
	private Vector3 targetPos;

	public float moveSpeed=8f;

	private Vector3 stageSize;
	private float objWidth;
	private float objHeight;

	private float xScreenLimit;
	private float yScreenLimit;

	private Rigidbody rb;
	// Use this for initialization

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		//stageSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));

		objWidth = transform.GetComponent<Renderer>().bounds.size.x;
		objHeight = transform.GetComponent<Renderer>().bounds.size.y;

		Debug.Log(stageSize+"-stageSize-"+stageSize.x+"-objWidth-"+stageSize.y+"--Screen.width--"+Screen.width+"---"+Camera.main.pixelWidth);

		xScreenLimit = stageSize.x - objWidth;
		yScreenLimit = stageSize.y - objHeight;

	}
	
	// Update is called once per frame
	void Update () 
	{
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");	
		targetPos = new Vector3(moveX,moveY,0)*Time.deltaTime*moveSpeed;
		//rb.velocity =targetPos;
		//Vector3 tmpPos = Camera.main.WorldToScreenPoint (transform.position);
		//Debug.Log(tmpPos+"-targetPos-");

		transform.Translate(targetPos);
	

	}

	void FixedUpdate()
	{
		
		//transform.position = Vector3.MoveTowards(transform.position,targetPos,Time.fixedDeltaTime*moveSpeed);
	}
}
