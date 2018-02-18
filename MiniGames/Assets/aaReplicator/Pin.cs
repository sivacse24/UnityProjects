using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	private bool isPinned = false;

	public float speed=20f;
	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPinned)
		rb.MovePosition(rb.position+Vector2.up*speed*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag=="Rotator")
		{
			isPinned =true;
			transform.parent = col.transform;
			Score.pinCount++;
		}
		else if(col.tag=="Pin")
		{
			FindObjectOfType<GameManager>().EndGame();
		}
	}
}
