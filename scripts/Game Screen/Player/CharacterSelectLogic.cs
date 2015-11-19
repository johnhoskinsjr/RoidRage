/*
 * This script controls the logic that sets
 * current character to selected character
 * 
 * Player selection Key
 * Johnny Green = 0
 * Girl = 1
 * Panda = 2
 * Jugg = 3
 * Demon = 4
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class CharacterSelectLogic : MonoBehaviour 
{
	public int			selectedCharacter; // this hold the variable for which character the player has selcted
	public GameObject	johnnyGreen;
	public	GameObject	aurora;
	public GameObject	panda;
	public GameObject	demon;
	public GameObject	jugg;
	public GameObject	auroraBackground;
	public GameObject	pandaBackground;
	public GameObject	juggBackground;
	public GameObject	demonBackground;
	public GameObject	johnnyBackground;
	int 				juggAbility;

	public int			i = 1;

	// Use this for initialization
	void Start () 
	{
		if(PlayerPrefs.HasKey ("SelectedCharacter"))
		{
			selectedCharacter = PlayerPrefs.GetInt ("SelectedCharacter");
		}
		else{
			selectedCharacter = 0;
		}

		switch(selectedCharacter)
		{
		case 0:
			JohnnyGreen ();
			break;

		case 1:
			Girl ();
			break;

		case 2:
			if(!SingletonSaveData.instance.PandaTrailActive && SingletonSaveData.instance.PandaLocked)
			{
				JohnnyGreen ();
			}
			else
			{
				Panda();
			}
			break;

		case 3:
			if(!SingletonSaveData.instance.JuggTrailActive && SingletonSaveData.instance.JuggLocked)
			{
				JohnnyGreen ();
			}
			else
			{
				Jugg ();
			}
			break;

		case 4:
			if(!SingletonSaveData.instance.DemonTrailActive && SingletonSaveData.instance.DemonLocked)
			{
				JohnnyGreen ();
			}
			else
			{
				Demon ();
			}
			break;
		}
	}

	void JohnnyGreen()
	{
		Instantiate (johnnyGreen);
		johnnyBackground.SetActive (true);
		PlayerPrefs.SetInt ("SelectedCharacter", 0);
		UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
			{ "johnnyGreenSelected",  i}
		});
	}

	void Girl()
	{
		Instantiate (aurora);
		auroraBackground.SetActive (true);
		UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
			{ "auroraSelected",  i}
		});
	}

	/*
	 * Manages free trail for the characters
	 */ 
	void Jugg()
	{
		if(SingletonSaveData.instance.JuggTrailActive)
		{
			// Initialize count and the set it
			int juggCount;
			if(PlayerPrefs.HasKey("JuggTrailCount"))
			{
				juggCount = PlayerPrefs.GetInt ("JuggTrailCount");
			}
			else
			{
				juggCount = 3;
			}
			//Subtract one from count
			juggCount --;
			 
			//If count is zero reset count to 3 and end free trail
			if(juggCount == 0)
			{
				SingletonSaveData.instance.JuggTrailActive = false;
				juggCount = 3;
			}
			//Save count
			PlayerPrefs.SetInt ("JuggTrailCount", juggCount);
		}

		Instantiate (jugg);
		juggBackground.SetActive (true);
		UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
			{ "juggSelected",  i}
		});
	}
	void Panda()
	{
		if(SingletonSaveData.instance.PandaTrailActive)
		{
			int pandaCount;
			if(PlayerPrefs.HasKey("PandaTrailCount"))
			{
				pandaCount = PlayerPrefs.GetInt ("PandaTrailCount");
			}
			else
			{
				pandaCount = 3;
			}
			
			pandaCount --;
			
			if(pandaCount == 0)
			{
				SingletonSaveData.instance.PandaTrailActive = false;
				pandaCount = 3;
			}
			
			PlayerPrefs.SetInt ("PandaTrailCount", pandaCount);
		}

		Instantiate (panda);
		pandaBackground.SetActive (true);
		UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
			{ "pandaSelected",  i}
		});
	}
	void Demon()
	{
		if(SingletonSaveData.instance.DemonTrailActive)
		{
			int demonCount;
			if(PlayerPrefs.HasKey("DemonTrailCount"))
			{
				demonCount = PlayerPrefs.GetInt ("DemonTrailCount");
			}
			else
			{
				demonCount = 3;
			}
			
			demonCount --;
			
			if(demonCount == 0)
			{
				SingletonSaveData.instance.DemonTrailActive = false;
				demonCount = 3;
			}
			
			PlayerPrefs.SetInt ("DemonTrailCount", demonCount);
		}

		Instantiate (demon);
		demonBackground.SetActive (true);
		UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
			{ "demonSelected",  i}
		});
	}
}
