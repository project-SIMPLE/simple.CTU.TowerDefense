using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour {

	public GameObject tileToSpawn;
	public GameObject referenceObject;
	public float timeOffset = 0.4f;
	public float randomValue = 0.8f;
	public float distanceBetweenTiles = 5.0f;
	private Vector3 previousTilePosition;
	private float startTime;
	private Vector3 direction, mainDirection = new Vector3(0,0,1), otherDirection = new Vector3(1,0,0);
	
	// Use this for initialization
	void Start () {
		previousTilePosition = referenceObject.transform.position;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > timeOffset)
		{
			if (Random.value < 0.8f)
				direction = mainDirection;
			else{
				Vector3 temp = direction;
				direction = otherDirection;
				mainDirection = direction;
				otherDirection = temp;
			}
			Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
			startTime = Time.time;
			Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0,0,0));
			previousTilePosition = spawnPos;
		}
	}
}
