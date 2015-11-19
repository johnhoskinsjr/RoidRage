/*
 * This script is obsolete, but stored for possible future use.
 * 
 * This script handles everything that deals with the swipe functions 
 * of this screen. It also handles clamping characters, and knowing
 * where the first and the last character are. Anything that needs to 
 * respond to swiping will be handled in this script
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterLeftAndRight : MonoBehaviour 
{
	public Vector3		farLeft; // Holds transform of first character
	public Vector3		farRight; // Holds transform of last character

	private bool		unlockPanelOpen = false;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "UnlockPanel");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(unlockPanelOpen)
			return;
		Vector3 characterPos = transform.position;

		if(Input.touchCount == 1)
		{
			foreach(Touch touch in Input.touches)
			{
				/*
				 * moves the row of character based off sliding of finger
				 * and clamps the ends of the rows of characters.
				 */ 
				if(transform.position.x < farLeft.x || transform.position.x > farRight.x)
				{
					characterPos.x -= touch.deltaPosition.x / 50;
				}
			}
			characterPos.x = Mathf.Clamp (characterPos.x, farRight.x, farLeft.x);
			transform.position = characterPos;
		}
	}

	void UnlockPanel()
	{
		unlockPanelOpen = !unlockPanelOpen;
	}
}
