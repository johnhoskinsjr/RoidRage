/* 
 * This scripts main purpose is to first check to see
 * if a black hole is ready to spawn. If a black hole is ready to
 * spawn, then put all spawn points in a list. From the list 
 * randomly pull one spawn point. Then instantiate a black hole.
 * Make spawn point a parent of black hole. Then set black hole
 * local position to zero.
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateBlackHoleForSection : MonoBehaviour 
{
	List<Transform> 			spawnPoints = new List<Transform>();
	public  GameObject 			blackHole;

	/*
	 * Checks to see if the black hole is ready to spawn
	 */ 
	void OnEnable()
	{
		/*
		 * This foreach loops checks to see if there is already
		 * a black hole game object, and if there is set inactive
		 */ 
		foreach(Transform child in transform)
		{
			if(child.name == "Blackhole_Spawn")
			{
				foreach(Transform newChild in child)
				{
					if(newChild.name == "Black Hole(Clone)")
					{
						newChild.gameObject.SetActive (false);
					}
				}
			}
		}

		/*
		 * Checks to see if black hole is ready to spawn
		 */ 
		if(!BlackHoleSpawner.GetNeedBlackHole () && !BlackHoleSpawner.GetBlackHoleBuilding ())
		{
			CreateBlackHole();
		}
	}

	/*
	 * Puts all spawn points in a list
	 */ 
	void CreateBlackHole ()
	{
		foreach(Transform child in transform)
		{
			if(child.name == "Blackhole_Spawn")
			{
				spawnPoints.Add (child);
			}
		}
		pickSpawnPointFromList();
		BlackHoleSpawner.SetNeedBlackHole(true);
		
	}

	/*
	 * Picks a random spawn point from list, instantiates black hole,
	 * then makes spawn point parent object, and finally sets local
	 * position of black hole
	 */ 
	void pickSpawnPointFromList ()
	{
		int i = Mathf.FloorToInt (Random.Range (0, spawnPoints.Count));
		GameObject blackHoleGO = Instantiate (blackHole) as GameObject;
		blackHoleGO.transform.parent = spawnPoints[i];
		blackHoleGO.transform.localPosition = Vector3.zero;
	}

	/*
	 * This is an OBSERVER METHOD
	 */ 
	void GameOver()
	{
		Destroy (this);
	}
}
