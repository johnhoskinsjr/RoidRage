/*
 *Script that handles what happens to asteroid when crashed into
 */

using UnityEngine;
using System.Collections;

public class AsteroidInstructions : MonoBehaviour 
{

	/*
	 * This method controls what happens player collides 
	 * with an asteroid. The method runs gameobject tag
	 * through switch statement for posting notification
	 */ 
	void OnTriggerEnter2D( )
	{
		NotificationCentre.PostNotification (this, "Crashed");
		Destroy (gameObject);
	}

	void PhaseDone()
	{
		Destroy (gameObject);
	}

	void Start()
	{
		NotificationCentre.AddObserver (this, "PhaseDone");
	}
}
