/*
 * This script controls the movement of the player, and
 * calibrates the accelerometer to the current position of the phone
 */ 

using UnityEngine;
using System.Collections;

/*
 * This class makes inspector cleaner
 */ 
[System.Serializable]
public class Boundary
{
	public float yMin, yMax;
}

public class PlayerMovement : MonoBehaviour 
{
	public  float		speedX; // The player speed along X axis
	public 	Boundary	boundary;// The boundary for the Y axis clamp

	private Quaternion 	calibrationQuaternion;
	private	float 		pixelRatio;
	private Vector3 	rightEdge;
	private Vector3 	playerPos;

	public  float 		secondsBetweenDoubleSpeed;
	private float 		firstDoubleSpeed;
	private bool		gameStarted = false;
	public 	float 		speedY;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "GameOver");
		NotificationCentre.AddObserver (this, "GameUnpaused");
		NotificationCentre.AddObserver (this, "StartGame");

		CalibrateAccelerometer ();

		/*
		 * This formula converts the screen size to unity units
		 * It gives you the right edge in unity pixels
		 */ 
		pixelRatio = (Camera.main.orthographicSize * 2f) / Camera.main.pixelHeight;
		rightEdge = new Vector3 (Screen.width * pixelRatio, -4f, 0f);
		playerPos = transform.position;
		speedX = 4;

		// does arithmetic for figuring out incrementation for wall speed based off set speed
		firstDoubleSpeed = speedX / secondsBetweenDoubleSpeed;

	}

	/*
	 * Calibrates the accelerometer based off
	 * current phone rotation
	 */ 
	void CalibrateAccelerometer ()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	/*
	 * Takes the difference of current accelerometer reading and accual 
	 * accelerometer reading
	 */ 
	Vector3 FixAcceleration (Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}



	void FixedUpdate()
	{
		// This is the real accelerometer reading
		Vector3 accelerationRaw = Input.acceleration;
		// This is the fixed accelerometer reading
		Vector3 acceleration = FixAcceleration (accelerationRaw);

		/*
		 * Only increases player X axis speed if game has started
		 */ 
		if(gameStarted)
		{
			if(speedX > 0)
			{
				speedX += firstDoubleSpeed * Time.deltaTime;
			}
			else{
				speedX -= firstDoubleSpeed * Time.deltaTime;
			}
		}
		// Changes the X axis direction when player reaches bounds
		if (playerPos.x >= rightEdge.x - playerPos.x - 1f || playerPos.x <= -rightEdge.x - playerPos.x + 1f) 
		{
			speedX = -speedX;
			NotificationCentre.PostNotification (this, "Turn");
		} 

		// Stops reading Y input when clamp to stop jumpy clamping
		if(playerPos.y < boundary.yMax || playerPos.y > boundary.yMin)
		{
			playerPos.y += (acceleration.y * speedY) * Time.fixedDeltaTime;
		}

		playerPos.x += speedX * Time.fixedDeltaTime;
		playerPos.y = Mathf.Clamp (playerPos.y, -4.5f, 4.5f);
		transform.position = playerPos;
	}

	/*
	 * OBSERVER METHOD
	 */
	void GameOver()
	{
		Destroy (this);
	}

	/*
	 * OBSERVER METHOD
	 */ 
	void GameUnpaused()
	{
		CalibrateAccelerometer ();
	}

	/*
	 * OBSERVER METHOD
	 */
	void StartGame()
	{
		gameStarted = true;
	}
}
