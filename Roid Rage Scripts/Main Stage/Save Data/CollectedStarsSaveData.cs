/*
 * This scripts purpose is to keep track of save data
 * for total stars collected, current stars collected
 * and instantiating game over collected stars animation
 */ 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;


public class CollectedStarsSaveData : MonoBehaviour 
{
	public GameObject	starTextGO;
	public int			currentStarsCollected;
	public int			totalStarsCollected = SingletonSaveData.instance.TotalStarsCollected;
	Text				starText;
	public AudioSource	starSound;
	public float		waitTimeBetweenStars = 1f;

	private const string 	SMOOTH_LANDING 				= "CgkImaKMrMQBEAIQEg";
	private const string 	ANOTHER_SMOOTH_LANDING 		= "CgkImaKMrMQBEAIQEw";
	private const string 	CANT_GET_ANY_SMOOTHER		= "CgkImaKMrMQBEAIQFA";


	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "StarCollected");
		NotificationCentre.AddObserver (this, "GameOverStarAnimation");
		NotificationCentre.AddObserver (this, "AsteroidCrash");

		starText = starTextGO.GetComponent<Text>();
		currentStarsCollected = 0;
		totalStarsCollected = SingletonSaveData.instance.TotalStarsCollected;

		starText.text = "" + SingletonSaveData.instance.TotalStarsCollected;
	}

	/*
	 * OBSERVER METHOD
	 * called when a star is collected
	 * increments current star collected by 1
	 */ 
	void StarCollected()
	{
		currentStarsCollected ++;
	}

	/*
	 * OBSERVER METHOD
	 * called when game is over
	 * used to save data
	 */ 
	void AsteroidCrash()
	{
		GooglePlayManager.instance.IncrementAchievementById (SMOOTH_LANDING, 1);
		GooglePlayManager.instance.IncrementAchievementById (ANOTHER_SMOOTH_LANDING, 1);
		GooglePlayManager.instance.IncrementAchievementById (CANT_GET_ANY_SMOOTHER, 1);
		UnityAnalytics.CustomEvent("Stars", new Dictionary<string, object>{
			{ "starsPerGame",  currentStarsCollected}
		});
		SingletonSaveData.instance.TotalStarsCollected = currentStarsCollected;
	}

	/*
	 * OBSERVER METHOD
	 * called when game is ready for counting
	 * the number of stars collected during session
	 */ 
	void GameOverStarAnimation()
	{
		StartCoroutine (CountStars());
	}

	/*
	 * This coroutine is used to allow time between each
	 * count of the stars
	 */ 
	IEnumerator CountStars()
	{
		for(int i = 0; i < currentStarsCollected; i++)
		{
			yield return new WaitForSeconds(waitTimeBetweenStars);
			totalStarsCollected ++;
			if(totalStarsCollected > 999)
			{
				totalStarsCollected = 999;
			}
			starText.text = "" + totalStarsCollected;
			starSound.PlayOneShot (starSound.clip);
		}
	}
}
