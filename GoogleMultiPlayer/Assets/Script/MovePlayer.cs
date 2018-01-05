﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;


public class MovePlayer : MonoBehaviour,RealTimeMultiplayerListener {

	[SerializeField]
	private SliderBar hSlider,vSlider;
	[SerializeField]
	private float playerMoveSpeed=10;


	[SerializeField]
	private GameObject playerPrefeb;

	private bool isAuthenicatedOnce=false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isAuthenicatedOnce)
		{
			isAuthenicatedOnce = true;
			authenticateUser ();

		}
		
		if(playerPrefeb!=null)
		{
			playerPrefeb.transform.Translate (new Vector3(hSlider.sliderValue,0,vSlider.sliderValue)*Time.deltaTime*playerMoveSpeed);
		}
	
	}

	private void authenticateUser()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		Debug.Log ("siva -- authenticateUser");

		PlayGamesPlatform.Instance.Authenticate((bool success) => {
			// handle success or failure
			Debug.Log ("siva -- Authenticate--"+success);

			if(success)
			{
				CreateQuickGame();
			}
			else
			{
				Debug.Log ("siva -- Failed to Login");
			}
		});
	}

	private void CreateQuickGame()
	{
		const int MinOpponents = 1, MaxOpponents = 2;
		const int GameVariant = 0;
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents,GameVariant, this);
	}

	#region RealTimeMultiplayerListener implementation

	public void OnRoomSetupProgress (float percent)
	{
		//throw new System.NotImplementedException ();
	}

	public void OnRoomConnected (bool success)
	{
		//throw new System.NotImplementedException ();
	}

	public void OnLeftRoom ()
	{
		//throw new System.NotImplementedException ();
	}

	public void OnParticipantLeft (Participant participant)
	{
		//throw new System.NotImplementedException ();
	}

	public void OnPeersConnected (string[] participantIds)
	{
		//throw new System.NotImplementedException ();
	}

	public void OnPeersDisconnected (string[] participantIds)
	{
		//throw new System.NotImplementedException ();
	}

	public void OnRealTimeMessageReceived (bool isReliable, string senderId, byte[] data)
	{
		//throw new System.NotImplementedException ();
	}

	#endregion
}
