using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector2 ballMoveForce;

	private Rigidbody2D rb;

	public GameObject nextBall;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(ballMoveForce,ForceMode2D.Impulse);
	}

	public void splitBall()
	{
		if(nextBall!=null)
		{
			GameObject rightBall = Instantiate(nextBall,rb.position+Vector2.right/4f,Quaternion.identity);
			GameObject lefgtBall = Instantiate(nextBall,rb.position+Vector2.left/4f,Quaternion.identity);

			print(rightBall.GetComponent<Ball>());
			rightBall.GetComponent<Ball>().ballMoveForce = new Vector2(2f,5f);
			lefgtBall.GetComponent<Ball>().ballMoveForce = new Vector2(-2f,5f);


		}
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
