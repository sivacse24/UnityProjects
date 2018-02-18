using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BubblePlayer : MonoBehaviour {

	public float speed = 4.0f;
	private Rigidbody2D rb;
	private float movePos;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		movePos = Input.GetAxisRaw("Horizontal")*speed;
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position+new Vector2(movePos*Time.fixedDeltaTime,0f));
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.tag=="Ball")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

}
