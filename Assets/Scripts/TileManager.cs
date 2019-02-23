using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	private Transform playerTransform;
	private float spawnZ = 0.0f;  //-2
	private float tileLength = 3.0f;
	private int amnTilesOnScreen=15;
	private float safeZone=10.0f;
	private int lastPreFabIndex = 0;
	//public GameObject coin;
	//public bool canadd=true;
	private int i;
	public GameObject[] star;
	private List<GameObject> activeTiles;
	private List<GameObject> stars;
	public bool hitstar=false;

	// Use this for initialization
	void Start () {
		i = 0;
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		activeTiles = new List<GameObject> ();
		stars = new List<GameObject> ();
		for (i = 0; i < amnTilesOnScreen; i++) {
			if (i < 5) {
				SpawnTile (Random.Range(0,2));

			} else {
				SpawnTile ();
				if (i == 10) {
					SpawnStar ();
				}

			}
		}
	}
		// Update is called once per frame

	void Update () {
		if (playerTransform.position.y < 0)
			return;
		//canadd = false;

		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Score> ().star)
			hitstar = true; 
		if (hitstar) {
			DeleteStar ();
			hitstar=false;
			//GetStar ();
		}
		if(playerTransform.position.z-safeZone> (spawnZ - amnTilesOnScreen *tileLength)){
			
				//SpawnTile (Random.Range (7, 18));
			SpawnTile ();
			DeleteTile ();

			if (stars.Count>0 && activeTiles [0].transform.position.z - 1.5 == stars [0].transform.position.z) {
				DeleteStar ();
			}
			if (i == 10 || i == 20){
				SpawnStar ();
				i=0;
			}
			i++;
		}
	}
	//used for spawning tiles
	private void SpawnTile(int prefabindex=-1){
		GameObject go;
		if (prefabindex==-1)
			go = Instantiate (tilePrefabs [RandomPrefabIndex()]) as GameObject;
		else
			go = Instantiate (tilePrefabs [prefabindex]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength; 
		activeTiles.Add (go);
	}
	private void DeleteTile(){
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}
	private void DeleteStar(){
		if (stars.Count > 0) {
			Destroy (stars [0]);
			stars.RemoveAt (0);
		}

	}
	private int RandomPrefabIndex(){
		if (tilePrefabs.Length <= 3)
			return Random.Range(0,1);
		int randomIndex = lastPreFabIndex;
		while ((randomIndex == lastPreFabIndex)||(lastPreFabIndex == 2 && randomIndex == 3)||(lastPreFabIndex == 3 && randomIndex == 2)||
			(randomIndex==6 && lastPreFabIndex==2)||(randomIndex==6 && lastPreFabIndex==3)||(randomIndex==3 && lastPreFabIndex==6)||(randomIndex==2 && lastPreFabIndex==6)) {
			randomIndex = Random.Range (0, 7);
		}
		lastPreFabIndex = randomIndex;

		return randomIndex;
	}
	private void SpawnStar(){
		
		GameObject go;
		int a = 0;
		if (lastPreFabIndex == 0) {
			a = 1;
		}
		else if (lastPreFabIndex == 1)
			a = Random.Range (0, 3);
		else if (lastPreFabIndex == 2)
			a = 0;
		else if (lastPreFabIndex == 3)
			a = 2;
		else if (lastPreFabIndex == 4)
			a = Random.Range (0, 2);
		else if (lastPreFabIndex == 5)
			a = Random.Range (1, 3);
		else {
			int k = Random.Range (0, 2);
			if (k == 0)
				a = 0;
			else
				a = 2;
		}
		go = Instantiate (star[a]) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = Vector3.forward * (spawnZ-tileLength/2);
		stars.Add (go);
	}

}
