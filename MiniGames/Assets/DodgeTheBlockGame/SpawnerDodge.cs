using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDodge : MonoBehaviour {

	public List<Transform> spawnerList;

	public GameObject blockPrefeb;
	// Use this for initialization

	public float nextBlockInterval =2f;
	public float nextBlockTime =0f;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time>=nextBlockTime)
		{
			nextBlockTime = Time.time+nextBlockInterval;
			spawnBlocks();
		}

	}

	void spawnBlocks()
	{
		int randNumber = Random.Range(0,spawnerList.Count);

		for(int i=0;i<spawnerList.Count;i++)
		{
			if(i!=randNumber)
			Instantiate(blockPrefeb,spawnerList[i].position,Quaternion.identity);
		}
	}
}
