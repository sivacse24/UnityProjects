using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
	private bool isGameEnd=false;

	public Rotator rotator;
	public Spawner spawner;

	public Animator animator;

	public void EndGame()
	{
		if(isGameEnd)
			return;

		rotator.enabled = false;
		spawner.enabled = false;

		animator.SetTrigger("EndGame");

		isGameEnd = true;
	}

	public void restartLevel()
	{
		//called from animation event
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
