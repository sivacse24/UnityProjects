using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public float jumpForce=5f;
	// Use this for initialization

	private SpriteRenderer sr;

	private string currentColor;
	public Color Cyan;
	public Color Yellow;
	public Color Red;
	public Color Blue;




	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		rb.isKinematic = true;
		getRandomColor();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")||Input.GetMouseButtonDown(0))
		{
			if(rb.isKinematic)
			{
				rb.isKinematic = false;
			}
			rb.velocity = Vector2.up*jumpForce;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag=="ChangeColor")
		{
			print("in...");
			Destroy(col.gameObject);
			getRandomColor();
			return;
		}
		if(col.tag!=currentColor)
		{
			print("gameOver");
		}

	}

	private void getRandomColor()
	{

		int randColorIndex = Random.Range(0,3);
		switch(randColorIndex)
		{
			case 0:
				currentColor="Cyan";
				sr.color = Cyan;
				break;
			case 1:
				currentColor="Yellow";
				sr.color = Yellow;
					break;
			case 2:
				currentColor="Red";
				sr.color = Red;
					break;
			case 3:
				currentColor="Blue";
				sr.color = Blue;
					break;
		}
	}
}
