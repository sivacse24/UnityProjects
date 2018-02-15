using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour {

	// Use this for initialization
	private LineRenderer lr;
	private EdgeCollider2D ec;

	private List<Vector2> points;
	public float pointSpacing =0.1f;
	public Transform head;
	void Start () 
	{
		lr = GetComponent<LineRenderer>();
		ec = GetComponent<EdgeCollider2D>();
		points = new List<Vector2>();
		addPoint();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Vector3.Distance(points.Last(),head.position)>pointSpacing)
			addPoint();
	}

	private void addPoint()
	{
		if(points.Count>1)
		ec.points = points.ToArray<Vector2>();

		points.Add(head.position);


		lr.positionCount = points.Count;
		lr.SetPosition(points.Count-1,head.position);


	}
}
