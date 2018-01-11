using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class MoveBullet : MonoBehaviour {

	private Rigidbody rb;
	private float moveSpeed=25f;

	[SerializeField]
	private float bulletDamage=10f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward*moveSpeed;
		Destroy(gameObject,2f);

	}

	void OnCollisionEnter(Collision collision)
	{
		Health objHealth = collision.gameObject.GetComponent<Health>();
		if(objHealth!=null)
			objHealth.takeDamage(bulletDamage);
		
		if(collision.collider.CompareTag("Player"))
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
