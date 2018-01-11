using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour {

	//this attached to canvas
	// Use this for initialization
	[SerializeField]
	private Image foreGround;
	void Start () {
		transform.LookAt(Camera.main.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reduceHealthBarSize(float ratio)
	{
		foreGround.rectTransform.localScale = new Vector3(ratio,1,1);
	}
}
