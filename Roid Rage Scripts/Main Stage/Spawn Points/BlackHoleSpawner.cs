/*
 * This cripts picks a random time for the next black hole to spawn.
 * Waits for this time, then tell create black hole script to create
 * a black hole.
 * 
 * KEY FOR BOOLEANS:
 * 
 * if(needBlackHole && BlackHoleBuilding) == This starts the timer on this script.
 * if(!needBlackHole && !blackHoleBuilding) == This tells section to create a black hole.
*/

using UnityEngine;
using System.Collections;

public class BlackHoleSpawner : MonoBehaviour 
{
	public static bool			needBlackHole = true; // set true by section script
	public float 				maxTimeBetweenBlackHole;
	public float 				minTimeBetweenBlackHole;
	public static bool 			blackHoleBuilding = true ; // when set to true creates a black hole variable only checked by
	float t = 0;
	float timer;

	/*
	 * Reseting of static variables on restart of game
	 */
	void Start () 
	{
		needBlackHole = true;
		blackHoleBuilding = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		 * This creates a random timer
		 */ 
		if(needBlackHole)
		{
			timer = PickTimeForNextBlackHole();
			SetBlackHoleBuilding (true);
			SetNeedBlackHole (false);
		}
		/*
		 * This waits for timer to run out
		 */ 
		if (blackHoleBuilding)
		{
			t += Time.deltaTime;
			if(t >= timer)
			{
				SetBlackHoleBuilding (false);
				t = 0;
			}
		}
	}

	/*
	 * Picks a random time for waiting til next black hole
	 */ 
	float PickTimeForNextBlackHole ()
	{
		float number = Random.Range (minTimeBetweenBlackHole, maxTimeBetweenBlackHole);
		return number;
	}

	/*
	 * These getters and setters are for communicating
	 * with Create Black Hole For Section script
	 */ 
	public static bool GetBlackHoleBuilding()
	{
		return blackHoleBuilding;
	}

	public static void SetBlackHoleBuilding(bool value)
	{
		blackHoleBuilding = value;
	}

	public static bool GetNeedBlackHole()
	{
		return needBlackHole;
	}

	public static void SetNeedBlackHole(bool value)
	{
		needBlackHole = value; 
	}

	void GameOver()
	{
		Destroy (this);
	}
}
