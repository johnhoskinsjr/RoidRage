using UnityEngine;
using System.Collections;

public class JustEndGame : MonoBehaviour 
{
	public GameObject 	image;
	public GameObject	particle;

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
		StartCoroutine (EndGame());
	}

	IEnumerator EndGame()
	{
		image.SetActive (false);
		particle.SetActive (true);
		yield return new WaitForSeconds(1.2f);
		particle.SetActive (false);
		yield return new WaitForSeconds(0.8f);
		Destroy (gameObject);
	}
}
