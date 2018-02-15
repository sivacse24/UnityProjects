using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform playerObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(playerObj.position.y>transform.position.y)
		{
			transform.position = new Vector3(transform.position.x,playerObj.position.y,transform.position.z);
		}
	}
}
