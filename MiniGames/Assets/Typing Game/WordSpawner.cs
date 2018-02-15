using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour {

	public GameObject wordPrefeb;
	public Transform wordCanvas;

	public WordDisplay SpawnWord()
	{
		Vector3 randPos = new Vector3(Random.Range(-4f,4f),7f);
		GameObject wordTxtObj = Instantiate(wordPrefeb,randPos,Quaternion.identity,wordCanvas);
		return wordTxtObj.GetComponent<WordDisplay>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
