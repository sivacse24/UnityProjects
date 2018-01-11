using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	[SerializeField]
	private float rotationSpeed = 150f;
	[SerializeField]
	private float moveSpeed = 10f;

	[SerializeField]
	private GameObject bulletPrefeb;
	[SerializeField]
	private Transform bulletSpanwPos;

	private Health myHealth;
	// Use this for initialization
	void OnEnabled()
	{
		
	}
	void Start () {
		myHealth = GetComponent<Health>();
		if(myHealth!=null)
		{
			myHealth.healthChangedEvent +=checkForUserLive;
		}

		if(isLocalPlayer)
			GetComponent<MeshRenderer>().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {

		if(!isLocalPlayer)
		{
			return;
		}
		//only local player will do this
		float x = Input.GetAxis("Horizontal")*Time.deltaTime*rotationSpeed;
		float z = Input.GetAxis("Vertical")*Time.deltaTime*moveSpeed;

		transform.Rotate(new Vector3(0,x,0));
		transform.Translate(new Vector3(0,0,z));

		if(Input.GetKeyDown(KeyCode.Space))
		{
			CmdFireBullet();
		}
	}
	[Command]//such function should be start with 'Cmd'
	private void CmdFireBullet()
	{
		GameObject bullet = Instantiate(bulletPrefeb,bulletSpanwPos.position,bulletSpanwPos.rotation);

		//sending all other player to spawn bullet
		NetworkServer.Spawn(bullet);
	}
	void OnDisable()
	{
		if(myHealth!=null)
		myHealth.healthChangedEvent -=checkForUserLive;
	}
	private void checkForUserLive(float myHealthVal)
	{
		if(myHealthVal<=0)
		{
			RpcResetPlayer();
			//Destroy(gameObject);
			myHealth.resetHealth();
		}
	}

	[ClientRpc]
	private void RpcResetPlayer()
	{
		if(isLocalPlayer)
			transform.position = Vector3.zero;//since this player has Network Transform all client/other players will get this position
	}
}
