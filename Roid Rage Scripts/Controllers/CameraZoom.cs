using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	private static bool		isGamePlaying = true;

	private static Vector3		blackholePos;
	private Vector3				cameraPos = new Vector3( 0, 0, -10);

	public float			lerpTime = 1f;
	private float			currentLerpTime = 0;

	/*
	 * Properties for the Black Hole script to send in position
	 */ 
	public static Vector3 BlackholePos {
		get {
			return blackholePos;
		}
		set {
			blackholePos = value;
			BlackHole ();
		}
	}


	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "GameOverStarAnimation");
	}

	/*
	 * Starts black hole animation
	 */ 
	public static void BlackHole()
	{
		isGamePlaying = false;
	}

	/*
	 * Resets camera back to normal state
	 */ 
	void GameOverStarAnimation()
	{
		isGamePlaying = true;
		transform.position = cameraPos;
		Camera.main.orthographicSize = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isGamePlaying)
			return;
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime)
		{
			currentLerpTime = lerpTime;
		}
		float t = currentLerpTime / lerpTime;
		transform.position = Vector3.Lerp (transform.position, blackholePos, t);
		Camera.main.orthographicSize -= 1f * (Time.deltaTime * 2.5f);
	}
}
