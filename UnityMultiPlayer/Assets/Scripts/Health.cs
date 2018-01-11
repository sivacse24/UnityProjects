using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Health : NetworkBehaviour {

	public delegate void HealthEvent(float healthVal);
	public event HealthEvent healthChangedEvent;

	//health attached to object
	[SerializeField]
	private int maxHealth =100;

	[SyncVar (hook = "reduceHealtBar")]public float currentHealth;

	[SerializeField]
	private float addDamage=1f;
	// Use this for initialization
	[SerializeField]
	HealthBar healthBar;
	void Start () {
		currentHealth = System.Convert.ToSingle(maxHealth) ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(float damageAmount)
	{
		if(!isServer)
		{
			//other player will not reduce damage.It wil receive damage from server
			return;
		}

		//current player only reduce the damage.Beacuse of SyncVar all player will get this info
		currentHealth -=damageAmount*addDamage;
		if(currentHealth<=0)
			currentHealth = 0;


		if(healthChangedEvent!=null)
		{			

			healthChangedEvent(currentHealth);
		}
		
	}

	public void reduceHealtBar(float curHealth)
	{
		//when change in 'currentHealth' from any player will trigger this function because of hook. 'currentHealth' will be passed in parameter curHealth
		healthBar.reduceHealthBarSize(curHealth/maxHealth);
	}

	public void resetHealth()
	{
		currentHealth = maxHealth;
	}
}
