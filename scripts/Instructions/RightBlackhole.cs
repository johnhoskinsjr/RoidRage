/*
 * This script handles the collider for right side blackhole trigger
 */

using UnityEngine;
using System.Collections;

public class RightBlackhole : MonoBehaviour 
{
	public Collider2D 		col;
	
	void Start()
	{
		NotificationCentre.AddObserver (this, "PhaseDone");
	}
	
	void OnTriggerEnter2D()
	{
		NotificationCentre.PostNotification (this, "RightSide");
	}
	
	void PhaseDone()
	{
		col.enabled = false;
	}
}
