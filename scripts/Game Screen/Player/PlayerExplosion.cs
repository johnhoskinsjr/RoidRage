/*
 * This script controls the players explosion
 */ 

using UnityEngine;
using System.Collections;

public class PlayerExplosion : MonoBehaviour 
{
	public GameObject		playerBody;
	public GameObject		playerBase;
	public GameObject		explosionParticle;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "AsteroidCrash");
		NotificationCentre.AddObserver (this, "BlackHole");
	}

	void BlackHole()
	{
		Destroy (gameObject);
	}
	
	void AsteroidCrash()
	{
		explosionParticle.SetActive (true);
		playerBody.SetActive (false);
		playerBase.SetActive (false);
	}
}
