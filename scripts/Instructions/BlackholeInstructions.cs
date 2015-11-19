/*
 * This script just handles the OnCollision Method
 */ 

using UnityEngine;
using System.Collections;

public class BlackholeInstructions : MonoBehaviour 
{
	void Start()
	{
		NotificationCentre.AddObserver (this, "PhaseDone");
	}

	void OnTriggerEnter2D()
	{
		NotificationCentre.PostNotification (this, "Blackhole");
	}
	void PhaseDone()
	{
		Destroy (gameObject);
	}
}
