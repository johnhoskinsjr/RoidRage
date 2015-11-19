using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class FreeStuffLogic : MonoBehaviour 
{
	private int 			advertCount;

	public GameObject		freeStuffButton;
	public GameObject		rewardsPanel;

	public int				i = 1;

	private const string	FIRST_PRIZE_POD = "CgkImaKMrMQBEAIQBQ";
	private const string	MORE_PRIZE_PODS = "CgkImaKMrMQBEAIQBg";
	private const string	MANY_PRIZE_POD 	= "CgkImaKMrMQBEAIQBw";

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "GameOver");
		Advertisement.Initialize ("37778", true);
	}

	/*
	 * What happens when the player presses the home button
	 */ 
	public void HomeButton()
	{
			StartCoroutine (LoadHome());
	}

	/*
	 * When the game over observer method is called
	 * it counts up to let game know when it is time
	 * to show an ad
	 */ 
	void GameOver()
	{
		//Sets ad count to player prefs
		if(PlayerPrefs.HasKey ("AdvertCount"))
		{
			advertCount = PlayerPrefs.GetInt ("AdvertCount");
		}
		else
		{
			advertCount = 4;
		}
		//incrementer when a game over happens
		advertCount++;

		//resets the ad count at 5 and at 4 tell app to display ad
		if(advertCount >= 5)
		{
			advertCount = 0;
		}
		else if(advertCount == 4)
		{
			freeStuffButton.SetActive (true); // sets the free stuff button active
		}
		PlayerPrefs.SetInt ("AdvertCount", advertCount); // sets the player prefs
	}

	/*
	 * What happens when the player presses the retry button on the game over screen
	 */ 
	public void Retry()
	{
		if(advertCount == 4)
		{
			if(Advertisement.isReady()) {
				Advertisement.Show();
				UnityAnalytics.CustomEvent("Ads", new Dictionary<string, object>{
					{ "noFreeStuff",  i}
				});

			}
		}
		StartCoroutine(LoadLevel ());
	}

	/*
	 * What happens when the player presses the free stuff button
	 */ 
	public void FreeStuff()
	{
		UnityAnalytics.CustomEvent("Ads", new Dictionary<string, object>{
			{ "freeStuff",  i}
		});
		if(Advertisement.isReady ())
		{
			Advertisement.Show ("rewardedVideoZone");
		}
		StartCoroutine (Rewards ());
		advertCount++;
	}

	/*
	 * Makes sure game doesn't restart until the ad is done showing
	 */ 
	IEnumerator LoadLevel ()
	{
		while(Advertisement.isShowing)
		{
			yield return null;
		}
		Application.LoadLevel ("Game");
	}

	/*
	 * Makes sure ad isn't playing before loading the the title screen
	 */ 
	IEnumerator LoadHome()
	{
		while(Advertisement.isShowing)
		{
			yield return null;
		}

		Application.LoadLevel ("Title");
	}

	/*
	 * Sets the rewards panel active if player has watched an unskippable ad
	 * from the free stuff button
	 */ 
	IEnumerator Rewards()
	{
		freeStuffButton.SetActive (false);
		advertCount = 0;
		PlayerPrefs.SetInt ("AdvertCount", advertCount);
		while(Advertisement.isShowing)
		{
			yield return null;
		}
		SingletonSaveData.instance.Save ();
		rewardsPanel.SetActive (true);

		yield return new WaitForSeconds(0.5f);
		GooglePlayManager.instance.UnlockAchievementById(FIRST_PRIZE_POD);
		GooglePlayManager.instance.IncrementAchievementById (MORE_PRIZE_PODS, 1);
		GooglePlayManager.instance.IncrementAchievementById (MANY_PRIZE_POD, 1);
	}
}
