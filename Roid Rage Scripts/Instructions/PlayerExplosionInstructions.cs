/*
 * The script for the player explosion instructions, when the player collides 
 * with the asteroid and the blackhole
 */ 

using UnityEngine;
using System.Collections;

public class PlayerExplosionInstructions : MonoBehaviour 
{
	public GameObject		playerBody;
	public GameObject		playerBase;
	public GameObject		explosionParticle;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "Crashed");
		NotificationCentre.AddObserver (this, "Blackhole");
	}

	/*
	 * OBSERVER METHOD
	 * 
	 * WHen player crashes into asteroid
	 */ 
	void Crashed()
	{
		playerBody.SetActive (false);
		playerBase.SetActive (false);
		explosionParticle.SetActive (true);
		StartCoroutine (RestartPlayer());
	}

	/*
	 * Coroutine that resets player when crashes into asteroid or blackhole
	 */ 
	IEnumerator RestartPlayer()
	{
		yield return new WaitForSeconds(1.9f);
		explosionParticle.SetActive (false);
		yield return new WaitForSeconds(2.0f);
		playerBase.SetActive (true);
		playerBody.SetActive (true);
	}

	/*
	 * OBSERVER METHOD
	 * 
	 * when player crashes into blackhole
	 */ 
	void Blackhole()
	{
		playerBody.SetActive (false);
		playerBase.SetActive (false);
		StartCoroutine (RestartPlayer());
	}
}
