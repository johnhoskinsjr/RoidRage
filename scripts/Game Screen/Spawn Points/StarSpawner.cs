/*
 * This cripts picks a random time for the next star to spawn.
 * Waits for this time, then tell create star script to create
 * a star.
 * 
 * KEY FOR BOOLEANS:
 * 
 * if(needStar && starBuilding) == This starts the timer on this script.
 * if(!needStar && !starBuilding) == This tells section to create a star.
*/


using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour 
{
	public static bool			needStar = true; // set true by section script
	public float 				maxTimeBetweenStar;
	public float 				minTimeBetweenStar;
	public static bool 			starBuilding = true ; // when set to true creates a black hole variable only checked by
	float t = 0;
	float timer;
	
	/*
	 * Reseting of static variables on restart of game
	 */
	void Start () 
	{
		needStar = true;
		starBuilding = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		 * This creates a random timer
		 */ 
		if(needStar)
		{
			timer = PickTimeForNextBlackHole();
			SetStarBuilding (true);
			SetNeedStar (false);
		}
		/*
		 * This waits for timer to run out
		 */ 
		if (starBuilding)
		{
			t += Time.deltaTime;
			if(t >= timer)
			{
				SetStarBuilding (false);
				t = 0;
			}
		}
	}
	
	/*
	 * Picks a random time for waiting til next star
	 */ 
	float PickTimeForNextBlackHole ()
	{
		float number = Random.Range (minTimeBetweenStar, maxTimeBetweenStar);
		return number;
	}
	
	/*
	 * These getters and setters are for communicating
	 * with Create Star For Section script
	 */ 
	public static bool GetStarBuilding()
	{
		return starBuilding;
	}
	
	public static void SetStarBuilding(bool value)
	{
		starBuilding = value;
	}
	
	public static bool GetNeedStar()
	{
		return needStar;
	}
	
	public static void SetNeedStar(bool value)
	{
		needStar = value; 
	}
	
	void GameOver()
	{
		Destroy (this);
	}
}
