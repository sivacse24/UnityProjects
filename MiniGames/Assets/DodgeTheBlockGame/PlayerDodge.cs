using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour {

	public float moveSpeed=15f;
	
	private Rigidbody2D rb;
	private float xMove;
	float myWidthOffset;
	public GameManagerDodge gameManager;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Debug.Log("GameManagerDodge.stageSize"+GameManagerDodge.stageSize);
		myWidthOffset = gameObject.GetComponent<Renderer>().bounds.size.x/2;//width of the player
	}
	
	// Update is called once per frame
	void Update () {

		xMove = Input.GetAxis("Horizontal");
	}

	void FixedUpdate()
	{
		Vector2 newPos = rb.position+Vector2.right*xMove*Time.fixedDeltaTime*moveSpeed;

		if(newPos.x>(-GameManagerDodge.stageSize.x+myWidthOffset) && newPos.x<(GameManagerDodge.stageSize.x-myWidthOffset))
			rb.MovePosition(newPos);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		
		if(col.collider.tag=="Ball")
			gameManager.gameOver();

		//this.enabled=false;
	}
}
