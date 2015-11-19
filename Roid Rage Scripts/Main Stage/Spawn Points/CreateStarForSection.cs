/* 
 * This scripts main purpose is to first check to see
 * if a star is ready to spawn. If a star is ready to
 * spawn, then put all spawn points in a list. From the list 
 * randomly pull one spawn point. Then instantiate a star.
 * Make spawn point a parent of star. Then set star
 * local position to zero.
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateStarForSection : MonoBehaviour 
{
	List<Transform> 			spawnPoints = new List<Transform>();
	public  GameObject 			star;
	
	/*
	 * Checks to see if the star is ready to spawn
	 */ 
	void OnEnable()
	{
		/*
		 * This foreach loops checks to see if there is already
		 * a star game object, and if there is set inactive
		 */ 
		foreach(Transform child in transform)
		{
			if(child.name == "Star_Spawn")
			{
				foreach(Transform newChild in child)
				{
					if(newChild.name == "Star(Clone)")
					{
						newChild.gameObject.SetActive (false);
					}
				}
			}
		}
		
		/*
		 * Checks to see if star is ready to spawn
		 */ 
		if(!StarSpawner.GetNeedStar () && !StarSpawner.GetStarBuilding ())
		{
			CreateStar();
		}
	}
	
	/*
	 * Puts all spawn points in a list
	 */ 
	void CreateStar ()
	{
		foreach(Transform child in transform)
		{
			if(child.name == "Star_Spawn")
			{
				spawnPoints.Add (child);
			}
		}
		pickSpawnPointFromList();
		StarSpawner.SetNeedStar(true);
		
	}
	
	/*
	 * Picks a random spawn point from list, instantiates star,
	 * then makes spawn point parent object, and finally sets local
	 * position of star
	 */ 
	void pickSpawnPointFromList ()
	{
		int i = Mathf.FloorToInt (Random.Range (0, spawnPoints.Count));
		GameObject starGO = Instantiate (star) as GameObject;
		starGO.transform.parent = spawnPoints[i];
		starGO.transform.localPosition = Vector3.zero;
	}
	
	/*
	 * This is an OBSERVER METHOD
	 */ 
	void GameOver()
	{
		Destroy (this);
	}
}
