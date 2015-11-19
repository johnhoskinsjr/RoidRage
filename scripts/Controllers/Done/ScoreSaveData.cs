/*
 * 	This script is in control of keeping
 * 	track of the score and the text that 
 * 	displays score
 * 
 * 	Observers for this class is on a seperate script
 * 	attached to gameobject
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class ScoreSaveData : MonoBehaviour 
{
	private float		timer;
	private bool		gameStart = false;
	public  GameObject	timerTextGO; // variable that holds the gameobject for the timer text
	public GameObject	highscoreTextGO;
	public int			score;
	
	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "GameOver");
		NotificationCentre.AddObserver (this, "StartGame");
		NotificationCentre.AddObserver (this, "AsteroidCrash");
		NotificationCentre.AddObserver (this, "BlackHole");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Initialization of variables
		Text timerText = timerTextGO.GetComponent<Text> (); 

		 /*
		  *	starts timer when game starts
		  *	and adjust score text to display score
		  */
		if(gameStart)
		{
			// timer
			timer += Time.deltaTime;
			// converts timer into an integer for text
			score = Mathf.FloorToInt(timer) * 10;
			// sets timer text
			timerText.text = "" + score;
		}
	}

	/*
	 *	!!!! WARNING OBSERVER METHOD !!!!
	 *	 !! DO NOT CHANGE METHOD NAME !!
	 *
	 *	This method tells script to start keeping 
	 *	track of score
	 */
	void StartGame()
	{
		gameStart = true;
		timerTextGO.SetActive (true);
	}

	/*
	 *	!!!! WARNING OBSERVER METHOD !!!!
	 *	 !! DO NOT CHANGE METHOD NAME !!
	 *
	 *	This method tells script to stop keeping 
	 *	track of score
	 */
	void GameOver()
	{
		UnityAnalytics.CustomEvent("Score", new Dictionary<string, object>{
			{ "averageGameScore",  score},
		});
		gameStart = false;
	}

	void AsteroidCrash()
	{
		SingletonSaveData.instance.Highscore = score;
		SetHighscoreText ();
	}

	void SetHighscoreText()
	{
		Text highscoreText = highscoreTextGO.GetComponent <Text>();
		highscoreText.text = "" + SingletonSaveData.instance.Highscore;
	}

	void BlackHole()
	{
		SetHighscoreText ();
	}
}
