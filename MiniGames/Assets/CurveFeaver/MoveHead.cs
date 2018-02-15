using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHead : MonoBehaviour {

	// Use this for initialization
	public float moveSpeed=1f;
	public float rotateSpeed=200f;

	private float horizantal;
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		horizantal = Input.GetAxisRaw("Horizontal");
	}

	void FixedUpdate()
	{
		transform.Translate(Vector2.up*moveSpeed*Time.fixedDeltaTime);
		transform.Rotate(Vector3.forward*-horizantal*rotateSpeed*Time.fixedDeltaTime,Space.Self);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		print(col.tag);
	}
}
