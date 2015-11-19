/*
 *	This script only job is to control GUI elements
 *	for the Title Screen, such as the Play button
 *	and the Character Select button.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class TitleGUIController : MonoBehaviour {

	public AudioSource		buttonSound; // variable that holds the audio source for the button sound
	public GameObject		playButton;

	private const string 	GETTING_ANGRY 				= 	"CgkImaKMrMQBEAIQAQ";

	void Awake()
	{

	}

	void Start()
	{
		if(SingletonSaveData.instance.InstructionsCompleted)
		{
			playButton.SetActive (true);
		}
	}

	/* 
	 * 	This method controls actions 
	 * 	made when play button is pressed
	 */ 
	public void PlayButton()
	{
		LoadLoadingScreen();
	}

	/*
	 * 	This method controls actions made 
	 * 	when character select button is pressed
	 */ 
	public void InstructionsButton()
	{
		LoadInstructions();

	}

	/*
	 * 	This method pauses load level
	 * 	for button sound to have time to play
	 */ 
	void LoadLoadingScreen()
	{
		StartCoroutine (PlayButtonSound());
	}

	/*
	 * 	This method pauses load level
	 * 	for button sound to have time to play
	 */ 
	void LoadInstructions()
	{
		if(SingletonSaveData.instance.instructionsCompleted)
		{
			UnityAnalytics.CustomEvent("Instructions", new Dictionary<string, object>{
			{ "instructionsReplay", 1 }
			});
		}

		GPAchievement achievement = GooglePlayManager.instance.GetAchievement(GETTING_ANGRY);
		if(achievement.state.ToString() == "STATE_UNLOCKED" && !SingletonSaveData.instance.instructionsCompleted)
		{
			SingletonSaveData.instance.InstructionsCompleted = true;
			StartCoroutine (PlayButtonSound ());
		}
		else
		{
			StartCoroutine (InstructionsButtonSound());
		}
	}

	/*
	 * 	Coroutine that plays button sound
	 * 	and pauses allow time for sound
	 * 	then load loading screen
	 */ 
	IEnumerator PlayButtonSound()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		yield return new WaitForSeconds(.5f);
		Application.LoadLevel("Character_Select");
	}

	/*
	 * 	This method loads character select screen
	 */ 
	IEnumerator InstructionsButtonSound()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		yield return new WaitForSeconds(.5f);
		Application.LoadLevel ("Instructions");
	}
}
