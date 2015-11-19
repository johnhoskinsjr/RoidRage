/*
 * This script builds the stars, asteroids, and blackholes
 * for the tutorial.
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateStars : MonoBehaviour 
{
	public GameObject		stars; // Star prefab
	public GameObject		asteroid; // Asteroid Prefab
	public GameObject		blackhole; // Blackhole Prefab
	public GameObject		starImage; //Star image gameobject for star counter
	public GameObject		starCounter; // The counter that holds star count
	public GameObject		starParticles; // Particles for star counter

	List<GameObject>		starPoints = new List<GameObject>(); // list that holds the spawn points for the stars
	int 					i = 0; // Represents the current phase
	int						starsCollected= 0; // Number of stars collected to know when phase ends
	GameObject				asteroidSpawnPoint; // Holds asteroid spawn point
	public static int		numberAsteroidCrashes = 0; // Holds number of asteroid crashes
	public AudioSource		starSound; // Holds the audio source component for the stars
	Transform				leftSide; // Collider for the left side to know where to spawn blackhole.
	Transform				rightSide; // Collider for the right side to know where to spawn blackhole.
	Transform				blackholeFirstPoint; // holds transform for the initial spawn point of blackhole
	bool					isLeftSide; // Boolean that tells blackhole where to lerp to.
	GameObject				blackholeGO; // Holds the blackhole Gameobject

	public float smoothDampTime;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "ReadyForStars");
		NotificationCentre.AddObserver (this, "PhaseThree");
		NotificationCentre.AddObserver (this, "Crashed");
		NotificationCentre.AddObserver (this, "StarCollected");
		NotificationCentre.AddObserver (this, "LeftSide");
		NotificationCentre.AddObserver (this, "RightSide");
		NotificationCentre.AddObserver (this, "StartBlackhole");

		// Assigns all children to a variable
		foreach(Transform child in transform)
		{
			starPoints.Add (child.gameObject);
			switch(child.name)
			{
			case "Asteroid":
				asteroidSpawnPoint = child.gameObject;
				break;

			case "Left":
				leftSide = child;
				break;

			case "Right":
				rightSide = child;
				break;

			case "Blackhole":
				blackholeFirstPoint = child;
				break;
			}

		}
	}

	/*
	 OBSERVER METHOD
	*/
	void ReadyForStars()
	{
		switch(i)
		{
			/*
			 * Spawns the first 4 stars
			 */ 
		case 0:
			starSound.PlayOneShot(starSound.clip);
			for(int j = 0; j < 4; j++)
			{
				CreateStar (j);
			}
			i++;
			break;

			/*
			 * Spawns stars and asteroid for phase 2
			 */ 
		case 1:
			starSound.PlayOneShot(starSound.clip);
			CreateStar (4);
			CreateAsteroid ();
			leftSide.gameObject.SetActive (true);
			rightSide.gameObject.SetActive (true);
			i++;
			break;

			/*
			 * Spawns stars, asteroid, and blackhole for phase 3
			 */ 
		case 2:
			// Create Star
			starSound.PlayOneShot(starSound.clip);
			CreateStar (4);
			// Create Asteroid
			CreateAsteroid ();
			if(isLeftSide)
			{
				MoveBlackhole(leftSide);
			}else{
				MoveBlackhole(leftSide);
			}
			i++;
			break;

			/*
			 * sets active the star counter and plays particles
			 */ 
		case 3:
			starSound.PlayOneShot (starSound.clip);
			starParticles.SetActive (true);
			starImage.SetActive (true);
			starCounter.SetActive (true);
			i++;
			break;

		default:
			break;
		}
	}

	/*
	 OBSERVER METHOD
	*/
	void LeftSide()
	{
		isLeftSide = true;
	}

	/*
	 OBSERVER METHOD
	*/
	void RightSide()
	{
		isLeftSide = false;
	}

	/*
	 OBSERVER METHOD
	*/
	void Crashed()
	{
		StartCoroutine (ResetAsteroid());
	}

	/*
	 OBSERVER METHOD
	*/
	void StartBlackhole()
	{
		blackholeFirstPoint.gameObject.SetActive (true);
		CreateBlackhole (blackholeFirstPoint);
	}

	/*
	 * Coroutine that resets asteroid when it is crashed into.
	 */ 
	IEnumerator ResetAsteroid()
	{
		yield return new WaitForSeconds(2.5f);
		CreateAsteroid ();
	}

	/*
	 OBSERVER METHOD
	*/
	void StarCollected()
	{
		starsCollected++;
		if(starsCollected == 4)
		{
			NotificationCentre.PostNotification (this, "PhaseDone");
		}
		else if(starsCollected == 5)
		{
			NotificationCentre.PostNotification (this, "PhaseDone");
		}
		else if(starsCollected == 6)
		{
			NotificationCentre.PostNotification (this, "PhaseDone");
		}
	}

	/*
	 * Method that instantiates asteroid at spawn point.
	 */ 
	void CreateAsteroid()
	{
		GameObject asteroidGO = Instantiate (asteroid) as GameObject;
		asteroidGO.transform.parent = asteroidSpawnPoint.transform;
		asteroidGO.transform.localPosition = Vector3.zero;
	}

	/*
	 * Method that instantiates Star at spawn point.
	 */ 
	void CreateStar(int k)
	{
		GameObject starGO = Instantiate (stars) as GameObject;
		starGO.transform.parent = starPoints[k].transform;
		starGO.transform.localPosition = Vector3.zero;
	}

	/*
	 * Method that instantiates blackhole at spawn point.
	 */ 
	void CreateBlackhole(Transform tran)
	{
		blackholeGO = Instantiate (blackhole) as GameObject;
		blackholeGO.transform.parent = tran;
		blackholeGO.transform.localPosition = Vector3.zero;
	}

	/*
	 * Starts coroutine that lerps blackhole to the side requested
	 */ 
	void MoveBlackhole (Transform tran)
	{
		StartCoroutine (LerpBlackhole(tran));
	}

	IEnumerator LerpBlackhole (Transform tran)
	{
		//yield return new WaitForSeconds(0.5f);
		float elapsedTime = 0;
		
		while(elapsedTime < smoothDampTime && transform.position != tran.position)
		{
			blackholeGO.transform.position = Vector3.Lerp (blackholeGO.transform.position, tran.position, elapsedTime / smoothDampTime);
			elapsedTime += Time.deltaTime;
			yield return new WaitForSeconds(.000001f);
		}
	}
}
