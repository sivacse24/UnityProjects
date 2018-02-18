using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapDestoryer : MonoBehaviour {

	public Tilemap tileMap;

	public Tile wallTile;
	public Tile destTile;

	public static MapDestoryer instance;

	public GameObject expAnimation;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Explode(Vector2 worldPos)
	{
		Vector3Int oriCell = tileMap.WorldToCell(worldPos);
		ExplodeCell(oriCell);

		if(ExplodeCell(oriCell+new Vector3Int(1,0,0)))
			ExplodeCell(oriCell+new Vector3Int(2,0,0));

		if(ExplodeCell(oriCell+new Vector3Int(0,1,0)))
			ExplodeCell(oriCell+new Vector3Int(0,2,0));

		if(ExplodeCell(oriCell+new Vector3Int(-1,0,0)))
			ExplodeCell(oriCell+new Vector3Int(-2,0,0));

		if(ExplodeCell(oriCell+new Vector3Int(0,-1,0)))
			ExplodeCell(oriCell+new Vector3Int(0,-2,0));


	}

	private bool ExplodeCell(Vector3Int cell)
	{
		Tile tile  = tileMap.GetTile<Tile>(cell);

		if(tile==wallTile)
		{
			return false;
		}
		else if(tile==destTile)
		{
			tileMap.SetTile(cell,null);
		}

		//create explosion
		Vector3 tileCellCenterPos = tileMap.GetCellCenterWorld(cell);

		GameObject go = Instantiate(expAnimation,tileCellCenterPos,Quaternion.identity);
		return true;
		//Destroy(go,1f);

	}
}
