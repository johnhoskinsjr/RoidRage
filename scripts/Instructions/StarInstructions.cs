/*
 * Handles the OnCollision for the stars
 */ 

using UnityEngine;
using System.Collections;

public class StarInstructions : MonoBehaviour {

	public GameObject		particle;
	private SpriteRenderer 	star;
	public AudioSource		starSound;
	public CircleCollider2D starCol;

	// The method controls what happens when player collides with star
	void OnTriggerEnter2D()
	{
		starSound.PlayOneShot (starSound.clip);
		// sends notification that star was collected
		NotificationCentre.PostNotification (this, "StarCollected");
		// this disables the star sprite so it appears star game object has disappeared
		star.enabled = false;
		starCol.enabled = false;
		// plays particle animation for collecting star
		particle.SetActive (false);
		particle.SetActive (true);
		StartCoroutine (Destroy());
	}

	// Use this for initialization
	void Start () 
	{
		// initializes star variable to equal the sprite renderer for the star
		star = gameObject.GetComponent<SpriteRenderer>();
		particle.SetActive (true);
	}
	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(1f);
		Destroy (gameObject);
	}
}
