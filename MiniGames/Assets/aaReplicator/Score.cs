﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
	public static int pinCount=0;
	public Text pinScoreText;
	// Use this for initialization
	void Start () {
		pinCount=0;
	}
	
	// Update is called once per frame
	void Update () {
		pinScoreText.text = pinCount.ToString();
	}
}
