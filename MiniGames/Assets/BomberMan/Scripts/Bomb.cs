using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private float bombExploreTime=2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bombExploreTime -= Time.deltaTime;

		if(bombExploreTime<=0)
		{
			Destroy(gameObject);
		}
	}
}
