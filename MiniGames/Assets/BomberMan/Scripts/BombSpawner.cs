using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour {

	[SerializeField]
	private Tilemap tileMap;

	[SerializeField]
	private GameObject bomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
		{
			Vector3 scToWr = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int tileCellPos= tileMap.WorldToCell(scToWr);
			Vector3 tileCellCenterPos = tileMap.GetCellCenterWorld(tileCellPos);

			Instantiate(bomb,tileCellCenterPos,Quaternion.identity);
		}
	}
}
