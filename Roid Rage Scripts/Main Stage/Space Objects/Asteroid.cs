using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public GameObject		asteroidExplosion;
	public SpriteRenderer 	asteroidSprite;
	public Collider2D		asteroidCollider1;
	public Collider2D		asteroidCollider2;
	public AudioSource		crumble;

	/*
	 * This method controls what happens player collides 
	 * with an asteroid. The method runs gameobject tag
	 * through switch statement for posting notification
	 */ 
	void OnTriggerEnter2D(Collider2D col )
	{
		StartCoroutine (Vibrate());
		//If current selected character is Jugg
		if(PlayerPrefs.GetInt ("SelectedCharacter") == 3)
		{
			if(SingletonSaveData.instance.JuggCrashes == 0)
			{
				SingletonSaveData.instance.JuggCrashes = 1;
				StartCoroutine (ExplodeAsteroid());
			}
			else if (SingletonSaveData.instance.JuggCrashes == 1)
			{
				SingletonSaveData.instance.JuggCrashes = 2;
				StartCoroutine (ExplodeAsteroid());
			}
			else
			{
				SingletonSaveData.instance.JuggCrashes = 0;
				NotificationCentre.PostNotification (this, "GameOver");
				NotificationCentre.PostNotification (this, "AsteroidCrash");
			}

		}
		// If any other character is selected
		else{
			NotificationCentre.PostNotification (this, "GameOver");
			NotificationCentre.PostNotification (this, "AsteroidCrash");
		}
	}

	IEnumerator ExplodeAsteroid()
	{
		asteroidCollider1.enabled = false;
		asteroidCollider2.enabled = false;
		crumble.PlayOneShot (crumble.clip);
		asteroidSprite.enabled = false;
		asteroidExplosion.SetActive (true);
		yield return new WaitForSeconds(2.0f);
		Destroy (gameObject);
	}

	IEnumerator Vibrate()
	{
		yield return new WaitForSeconds(0.07f);
		Handheld.Vibrate ();
	}
}
