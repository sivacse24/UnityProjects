using System.Collections;
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
	private Transform playerPrefeb;


	private Transform myPlayer;

	private bool isAuthenicatedOnce=false;
	// Use this for initialization
	void Start () 
	{
		//myPlayer = Instantiate (playerPrefeb,Vector3.zero,Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isAuthenicatedOnce)
		{
			isAuthenicatedOnce = true;
			authenticateUser ();

		}
		
		if(myPlayer!=null)
		{
			myPlayer.Translate (new Vector3(hSlider.sliderValue,0,vSlider.sliderValue)*Time.deltaTime*playerMoveSpeed);
			string playerPos = myPlayer.position.x + ":"+myPlayer.position.y + ":"+myPlayer.position.z;
			byte[] data = System.Text.ASCIIEncoding.Default.GetBytes (playerPos);
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll (false,data);
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
		Debug.Log ("siva -- CreateQuickGame");

		const int MinOpponents = 1, MaxOpponents = 2;
		const int GameVariant = 0;
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents,GameVariant, this);//next go to OnRoomSetupProgress
	}

	#region RealTimeMultiplayerListener implementation

	public void OnRoomSetupProgress (float percent)
	{
		//throw new System.NotImplementedException ();
		Debug.Log ("Siva -- OnRoomSetupProgress --"+ percent);
		if(percent>=20)
		{

			Debug.Log ("Siva--Waiting for other players to join");
			PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI ();//OnRoomConnected
		}
	}

	public void OnRoomConnected (bool success)
	{
		//throw new System.NotImplementedException ();
		Debug.Log ("Siva--OnRoomConnected-"+success);


		if(success)
		{
			myPlayer = Instantiate (playerPrefeb,Vector3.zero,Quaternion.identity);
			myPlayer.name = PlayGamesPlatform.Instance.RealTime.GetSelf ().ParticipantId;
			myPlayer.GetComponent <PlayerTextInfo>().updatePlayerName (PlayGamesPlatform.Instance.RealTime.GetSelf ().DisplayName);
			byte[] data = new byte[1];
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true,data);

		}
		else
		{
			CreateQuickGame ();
		}
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
		if (PlayGamesPlatform.Instance.RealTime.GetSelf ().ParticipantId.Equals (senderId))
			return;
		//throw new System.NotImplementedException ();
		if(isReliable)
		{
			Transform tmpObj =Instantiate (playerPrefeb,Vector3.zero,Quaternion.identity);
			tmpObj.name = senderId;
		}
		else
		{
			Transform targetObj = GameObject.Find (senderId).transform;

			if (targetObj == null)
				return;

			//targetObj.GetComponent <PlayerTextInfo>().updatePlayerName (PlayGamesPlatform.Instance.RealTime.GetSelf ().DisplayName);

			string postion = System.Text.ASCIIEncoding.Default.GetString (data);
			string[] rawPos = postion.Split (new string[1]{ ":" }, System.StringSplitOptions.RemoveEmptyEntries);
			Vector3 newPos = new Vector3 (
				System.Convert.ToSingle (rawPos [0]), 
				System.Convert.ToSingle (rawPos [1]), 
				System.Convert.ToSingle (rawPos [2])
			);

			targetObj.position = Vector3.Lerp (targetObj.position,newPos,Time.deltaTime*3.0f);
		}
	}

	#endregion
}
