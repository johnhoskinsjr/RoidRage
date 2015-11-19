/*
 * This script just pauses audio
 */ 

using UnityEngine;
using System.Collections;

public class InstructionsPauseAudio : MonoBehaviour 
{
	AudioSource 			music;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "Pause");
		music = gameObject.GetComponent<AudioSource>();
	}
	
	void Pause()
	{
		if(music.isPlaying)
		{
			music.Pause ();
		}
		else{
			music.UnPause ();
		}
	}
}
