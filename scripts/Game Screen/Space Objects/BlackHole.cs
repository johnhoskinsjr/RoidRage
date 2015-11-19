/*
 * This script just handles the OnCollision Method
 */ 

using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{
	public GameObject	gameoverParticle;

	void Start()
	{
		NotificationCentre.AddObserver (this, "GameOver");
		NotificationCentre.AddObserver (this, "GameOverStarAnimation");
	}

	void OnTriggerEnter2D()
	{
		GooglePlayManager.instance.UnlockAchievementById ("CgkImaKMrMQBEAIQCQ");
		gameoverParticle.SetActive (true);
		CameraZoom.BlackholePos = new Vector3(transform.position.x, transform.position.y, -10);
		NotificationCentre.PostNotification (this, "GameOver");
		NotificationCentre.PostNotification (this, "BlackHole");
	}

	void GameOverStarAnimation()
	{
		gameObject.SetActive (false);
	}
}
