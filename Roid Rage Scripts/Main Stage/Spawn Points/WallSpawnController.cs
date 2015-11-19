/*
 * This scripts main purpose is to spawn sections of the wall,
 * calculate speed at which sections should spawn and fall,
 * and destroys script when a game over happens.
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallSpawnController : MonoBehaviour 
{	
	public static float			waitTime = 4.5f; // wait time for sections to spawn. this variable is dynamic, and is based off of sectionSpeed
	private List<GameObject>	sectionList;	// List for the sections to populate
	public static float			sectionSpeed = 2f; // speed that the sections fall. This also controls waitTime for next section.
	public float				secondsBetweenDoubleSpeed = 100f;
	float						firstDoubleSpeed;  // These variables
	float						secondDoubleSpeed;	// are for incrementing
	float						thirdDoubleSpeed;	// the speed
	float						fourthDoubleSpeed;	// that the sections
	float						fifthDoubleSpeed;	// fall arithmetic done in start method
	int							i = 0;
	bool						isGamePlaying = true;

	// Use this for initialization
	void Start () 
	{
		// initializes variables
		NotificationCentre.AddObserver (this, "GameOver");
		Time.timeScale = 1;
		sectionList = new List<GameObject>();

		// resets static variables
		waitTime = 4.5f;
		sectionSpeed = 2f;

		// does arithmetic for figuring out incrementation for wall speed based off set speed
		firstDoubleSpeed = sectionSpeed / secondsBetweenDoubleSpeed;
		secondDoubleSpeed = (sectionSpeed * 2) / secondsBetweenDoubleSpeed;
		thirdDoubleSpeed = (sectionSpeed * 4) / secondsBetweenDoubleSpeed;
		fourthDoubleSpeed = (sectionSpeed / 8) / secondsBetweenDoubleSpeed;

		foreach(Transform child in transform)
		{
			sectionList.Add (child.gameObject);
		}

		StartCoroutine (CreateSection());
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		 * If statement that adjust incrementation
		 * according to the speed so that the speed doubles
		 * based off of set time
		 */ 
		if(sectionSpeed < 4){
			sectionSpeed += firstDoubleSpeed * Time.deltaTime;
		}
		else if(4 <= sectionSpeed && sectionSpeed < 8){
			sectionSpeed += secondDoubleSpeed * Time.deltaTime;
		}
		else if(8 <= sectionSpeed && sectionSpeed < 16){
			sectionSpeed += thirdDoubleSpeed * Time.deltaTime;
		}
		else{
			sectionSpeed += fourthDoubleSpeed * Time.deltaTime;
		}
	}

	/*
	 * Coroutine that spawns each section based off
	 * wait time which is the time needed to wait
	 * based off section speed
	 */ 
	IEnumerator CreateSection()
	{
		while(isGamePlaying)
		{
			waitTime = 9 / sectionSpeed; 
			sectionList[i].SetActive (true);
			i++;
			if(i >= sectionList.Count)
			{
				i = 0;
			}
			yield return new WaitForSeconds(waitTime);
		}
	}

	/*
	 * Destroys script when a game over happens
	 * this forces sections to stop spawning
	 */ 
	void GameOver()
	{
		Destroy (this);
	}

	/*
	 * public getter method for section movement
	 * script so the sections can update the speed
	 * they need to fall
	 */ 
	public static float GetSectionSpeed()
	{
		return sectionSpeed;
	}

	/*
	 * public getter for wait time so the section movement
	 * can make calculations for destroy time
	 */
	public static float GetWaitTime()
	{
		return waitTime;
	}
}
