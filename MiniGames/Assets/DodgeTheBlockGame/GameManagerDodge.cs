using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerDodge : MonoBehaviour {

	// Use this for initialization
	public float slowFactor=5f;
	public static Vector3 stageSize;
	void Start () {
		stageSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gameOver()
	{
		StartCoroutine(restartGame());
	}

	IEnumerator restartGame()
	{
		Time.timeScale = 1f/slowFactor;
		Time.fixedDeltaTime = Time.fixedDeltaTime/slowFactor;

		yield return new WaitForSeconds(3f/slowFactor);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime*slowFactor;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
