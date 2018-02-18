using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject pinPrefeb;

	private float nextFire=0f;
	private float nextFireRate=0.2f;
	// Use this for initialization
	void Start () {
		//nextFire = Time.time+firRate;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1") && nextFire<Time.time)
		{
			nextFire =Time.time+nextFireRate;
			spawnPin();
		}
	}

	private void spawnPin()
	{
		Instantiate(pinPrefeb,transform.position,transform.rotation);
	}
}
