using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerTextInfo : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private Text nameTxt;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updatePlayerName(string playerName)
	{
		nameTxt.text = playerName;
	}
}
