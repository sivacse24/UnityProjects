using UnityEngine;
using System.Collections;

public class ChainCollusision : MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D col)
	{
		Arrow.isFired = false;

		if(col.tag 	=="Ball")
		{
			col.GetComponent<Ball>().splitBall();
		}
	}
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

