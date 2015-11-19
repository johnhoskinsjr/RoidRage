/*
 * This script handles the collider for left side blackhole trigger
 */ 

using UnityEngine;
using System.Collections;

public class LeftBlackhole : MonoBehaviour 
{
	public Collider2D 		col;

	void Start()
	{
		NotificationCentre.AddObserver (this, "PhaseDone");
	}

	void OnTriggerEnter2D()
	{
		NotificationCentre.PostNotification (this, "LeftSide");
	}

	void PhaseDone()
	{
		col.enabled = false;
	}
}
