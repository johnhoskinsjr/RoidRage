using UnityEngine;
using System.Collections;

public class ScaleCharacter : MonoBehaviour 
{
	public Vector3 		minScale;
	public Vector3		maxScale;
	public GameObject	characterName;
	public GameObject	lockedTextGO;
	public GameObject	playButton;
	public GameObject	juggDescription;
	public GameObject	demonDescription;
	public GameObject	cameraGO;
	public Vector3		characterMin;
	public Vector3		characterMax;
	public Vector3		characterScaleUpPos;
	Vector3				cameraPos;
	[SerializeField] float 		lerpSpeed;

	Transform			character;

	// Use this for initialization
	void Start () 
	{
		//Grabs the image of character
		foreach(Transform child in transform)
		{
			character = child;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		cameraPos = cameraGO.transform.position;

		if(cameraPos.x < characterMax.x && cameraPos.x > characterMin.x)
		{
			//set characters name active
			characterName.SetActive (true);
			//initializes the characters pos
			Vector3 characterPos = character.position;
			//Scales character up
			character.localScale = maxScale;
			//Sets character to middle of the screen
			character.parent = cameraGO.transform;
			characterPos = characterScaleUpPos;
			//Sets characters pos
			character.localPosition = characterPos;
			/*
			 * Handles locking of characters and selecting of characters
			 */ 
			switch(character.name)
			{
			case "Johnny Green":
				PlayerPrefs.SetInt ("SelectedCharacter", 0); //Selects character for game
				lockedTextGO.SetActive (false); //Unlocks character
				playButton.SetActive (true); // Shows play button
				break;
				
			case "Girl":
				PlayerPrefs.SetInt ("SelectedCharacter", 1); // Selects character for game
				lockedTextGO.SetActive (false); // Unlocks character
				playButton.SetActive (true);// hides play button
				break;
				
			case "Panda":
				if(!SingletonSaveData.instance.PandaLocked || SingletonSaveData.instance.PandaTrailActive) 
				{
					PlayerPrefs.SetInt ("SelectedCharacter", 2); //selects character for game
					lockedTextGO.SetActive (false); // unlocks character
					playButton.SetActive (true); // show unlock button
				}
				else
				{
					NotificationCentre.PostNotification (this, "Panda"); //Selects character for for unlock screen
					lockedTextGO.SetActive (true); // locks character
					playButton.SetActive (false); // hides play button
				}	
				juggDescription.SetActive (false);
				break;
				
			case "Jugg":
				if(!SingletonSaveData.instance.JuggLocked || SingletonSaveData.instance.JuggTrailActive)
				{
					lockedTextGO.SetActive (false);
					playButton.SetActive (true);
					PlayerPrefs.SetInt ("SelectedCharacter", 3);
				}
				else
				{
					NotificationCentre.PostNotification (this, "Jugg");
					lockedTextGO.SetActive (true);
					playButton.SetActive (false);
				}
				demonDescription.SetActive (false);
				juggDescription.SetActive (true);
				break;
				
			case "Demon":
				if(!SingletonSaveData.instance.DemonLocked || SingletonSaveData.instance.DemonTrailActive)
				{
					lockedTextGO.SetActive (false);
					playButton.SetActive (true);
					PlayerPrefs.SetInt ("SelectedCharacter", 4);
				}
				else
				{
					NotificationCentre.PostNotification (this, "Demon");
					lockedTextGO.SetActive (true);
					playButton.SetActive (false);
				}
				juggDescription.SetActive (false);
				demonDescription.SetActive (true);
				break;
			}
		}
		else{
			/*
			 * What to do when the character isn't in the middle of the screen
			 */ 
			characterName.SetActive (false);
			character.localScale = minScale;
			character.parent = transform;
			//character.localScale = Vector3.Lerp (minScale, maxScale, 0.5f);
			character.localPosition = Vector3.zero;
		}
	}
}
