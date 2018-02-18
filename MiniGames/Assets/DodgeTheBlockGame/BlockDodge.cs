using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDodge : MonoBehaviour {

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y<-GameManagerDodge.stageSize.y-10f)
			Destroy(gameObject);
	}
}
