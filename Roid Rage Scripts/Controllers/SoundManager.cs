/*
 * 	This scripts job is to manage in-game
 * 	music
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
	private List<AudioSource>	inGameList;
	public AudioSource			inGameSound1;
	public AudioSource			inGameSound2;
	public AudioSource			inGameSound3;
	public AudioSource			inGameSound4;
	public AudioSource			inGameSound5;
	private int					playingMusic; // in game music that is currently playing, choosen from list randomly

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "GameOver");
		NotificationCentre.AddObserver (this, "GamePaused");
		NotificationCentre.AddObserver (this, "GameUnpaused");

		inGameList = new List<AudioSource> ();
		inGameList.Add (inGameSound1);
		inGameList.Add (inGameSound2);
		inGameList.Add (inGameSound3);
		inGameList.Add (inGameSound4);
		inGameList.Add (inGameSound5);
		
		playingMusic = Mathf.FloorToInt (Random.Range (0, 5));
		inGameList[playingMusic].Play ();
	}

	/*
	 *	!!!! WARNING OBSERVER METHOD !!!!
	 *	 !! DO NOT CHANGE METHOD NAME !!
	 *
	 *	This method is called when user dies.
	 *	This method stops music from playing
	 */
	void GameOver()
	{
		inGameList[playingMusic].Stop ();
	}

	/*
	 *	!!!! WARNING OBSERVER METHOD !!!!
	 *	 !! DO NOT CHANGE METHOD NAME !!
	 *
	 *	This method is called when user dies.
	 *	This method pauses game music when
	 *	game is paused
	 */
	void GamePaused()
	{
		inGameList[playingMusic].Pause ();
	}

	/*
	 *	!!!! WARNING OBSERVER METHOD !!!!
	 *	 !! DO NOT CHANGE METHOD NAME !!
	 *
	 *	This method is called when user dies.
	 *	This method unpauses game music when
	 *	game is unpaused
	 */
	void GameUnpaused()
	{
		inGameList[playingMusic].UnPause ();
	}
}
