using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInfoText : MonoBehaviour {

	[SerializeField]
	private Text infoText;
	// Use this for initialization
	void Start () {
		hideMsg(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showTextInfo(string msg)
	{
		Debug.Log ("siva--"+msg);
		infoText.text = msg;
		hideMsg (false);
	}

	public void hideMsg(bool val)
	{
		gameObject.SetActive (!val);
	}
}
