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

	[SerializeField]
	private UserInfoText userMsgCanvas;

	private Transform myPlayer;


	private bool isAuthenicatedOnce=false;
	// Use this for initialization
	void Start () 
	{
		//myPlayer = Instantiate (playerPrefeb,Vector3.zero,Quaternion.identity);
		userMsgCanvas.hideMsg (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("*-*-*"+Time.time);
		if(!isAuthenicatedOnce)
		{
			isAuthenicatedOnce = true;
			userMsgCanvas.showTextInfo ("Loading...");
			authenticateUser ();

		}
		
		if(myPlayer!=null && (hSlider.sliderValue!=0 || vSlider.sliderValue!=0))
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
		userMsgCanvas.hideMsg (false);
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
			userMsgCanvas.hideMsg (true);
			Debug.Log ("Siva--Waiting for other players to join");

			/*UserName  = Social.localUser.userName; // UserName
			UserID      = Social.localUser.id; // UserID
			ProfilePic    =Social.localUser.image; // ProfilePic*/


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
		//When my user do LeaveRoom (); this function called for me not for others.For other OnPeersDisconnected will be trigger
		userMsgCanvas.showTextInfo ("OnLeftRoom");

		//During gameplay the user may get disconnected from the room, and other participants may get disconnected or connected. It is not possible to reconnect to a room if this happens.
	}

	public void OnParticipantLeft (Participant participant)
	{
		//throw new System.NotImplementedException ();
		userMsgCanvas.showTextInfo ("OnParticipantLeft");

		//If a participant declines an invitation, the callback OnParticipantLeft is called. Since this happens before the game has begun, this can cause unbalanced calls relative to OnPeersConnected().
	}

	public void OnPeersConnected (string[] participantIds)
	{
		//throw new System.NotImplementedException ();
		userMsgCanvas.showTextInfo ("OnPeersConnected--"+getUserNameById(participantIds[0]));
		//If someone else gets connected or disconnected, your listener's OnPeersConnected and OnPeersDisconnected methods will be called:

	}

	public void OnPeersDisconnected (string[] participantIds)
	{
		//throw new System.NotImplementedException ();
		userMsgCanvas.showTextInfo ("OnPeersDisconnected--"+getUserNameById(participantIds[0]));
		foreach(string str in participantIds)
		{
			Debug.Log ("Siva---OnPeersDisconnected--"+getUserNameById(str));
		}
		//If someone else gets connected or disconnected, your listener's OnPeersConnected and OnPeersDisconnected methods will be called:
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


			/*Participant part;
			part.Player.userName;
			part.Player.id;
			part.Player.image;*/

			//string sendrName = getUserNameById (senderId);

			targetObj.GetComponent <PlayerTextInfo>().updatePlayerName (getUserNameById (senderId));

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

	private string getUserNameById(string senderId)
	{
		string userName = "noUserFound";
		List<Participant> participants = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
		foreach(Participant roomPlayer in participants)
		{
			if(roomPlayer.ParticipantId.Equals (senderId))
			{
				userName = roomPlayer.DisplayName;
				break;
			}
		}
		return userName;
	}

	public void onExitRoom()
	{
		PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
	}
	public void onQuitGame()
	{
		Application.Quit ();
	}
}
