/*
 * This script handles the actions of the pause panel
 */ 

using UnityEngine;
using System.Collections;

public class InstructionsPause : MonoBehaviour 
{
	bool				isPaused = false;
	public GameObject	pause;

	public void PauseInstructions()
	{
		isPaused = !isPaused;
		pause.SetActive (isPaused);
		NotificationCentre.PostNotification (this, "Pause");

	}
}
