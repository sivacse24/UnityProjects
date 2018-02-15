using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WordDisplay : MonoBehaviour {

	public Text text;
	public float fallSpeed=5f;

	public void SetWord(string word)
	{
		text.text=word;
	}
	public void removeLetter()
	{
		text.text = text.text.Remove(0,1);
		text.color = Color.red;
	}

	public void removeWord()
	{
		Destroy(gameObject);
	}

	void Start () {
		fallSpeed = Random.Range(0.5f,fallSpeed);
	}

	void Update () 
	{
		transform.Translate(0f,-fallSpeed*Time.deltaTime,0f);
	}
}
