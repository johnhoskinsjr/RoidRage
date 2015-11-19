/*
 * This script controls the movement of the player, and
 * calibrates the accelerometer to the current position of the phone
 * for the instructions
 */ 

using UnityEngine;
using System.Collections;

/*
 * This class makes inspector cleaner
 */ 

public class PlayerMovementInstructions : MonoBehaviour 
{
	public  float		speedX;
	public float 		smoothDampTime;

	private Quaternion 	calibrationQuaternion;
	private	float 		pixelRatio;
	private Vector3 	rightEdge;
	private Vector3 	playerPos;

	private bool		canXMove = false;
	private bool		canYMove = false;
	private bool		isPaused = false;
	public 	float 		speedY;

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "Crashed");
		NotificationCentre.AddObserver (this, "MoveY");
		NotificationCentre.AddObserver (this, "PhaseOne");
		NotificationCentre.AddObserver (this, "PhaseTwo");
		NotificationCentre.AddObserver (this, "PhaseThree");
		NotificationCentre.AddObserver (this, "PhaseDone");
		NotificationCentre.AddObserver (this, "Blackhole");
		NotificationCentre.AddObserver (this, "Pause");

		CalibrateAccelerometer ();

		/*
		 * This formula converts the screen size to unity units
		 * It gives you the right edge in unity pixels
		 */ 
		pixelRatio = (Camera.main.orthographicSize * 2f) / Camera.main.pixelHeight;
		rightEdge = new Vector3 (Screen.width * pixelRatio, -4f, 0f);

		speedX = 4;
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

		if(isPaused) return;
		// This is the real accelerometer reading
		Vector3 accelerationRaw = Input.acceleration;
		// This is the fixed accelerometer reading
		Vector3 acceleration = FixAcceleration (accelerationRaw);

		playerPos = transform.position;


		if(canXMove)
		{
			// Changes the X axis direction when player reaches bounds
			if (playerPos.x >= rightEdge.x - playerPos.x - 1f || playerPos.x <= -rightEdge.x - playerPos.x + 1f) 
			{
				speedX = -speedX;
			} 

			if(canYMove)
			{
				// Stops reading Y input when clamp to stop jumpy clamping
				if(playerPos.y < 4.5f || playerPos.y > -4.5f)
				{
					playerPos.y += (acceleration.y * speedY) * Time.fixedDeltaTime;
				}
			}

			playerPos.x += speedX * Time.fixedDeltaTime;
			playerPos.y = Mathf.Clamp (playerPos.y, -4.5f, 4.5f);
			transform.position = playerPos;
		}
	}

	/*
	 * OBSERVER METHOD
	 */
	void Crashed()
	{
		canXMove = false;
		StartCoroutine (ResetPlayerCrashed());
	}

	void Pause()
	{
		CalibrateAccelerometer ();
		isPaused = !isPaused;
	}

	void PhaseOne()
	{
		canXMove = true;
	}

	void PhaseDone()
	{
		canXMove = false;
		StartCoroutine (ResetPlayer());
	}

	void PhaseTwo()
	{
		CalibrateAccelerometer ();
		canXMove = true;
	}

	void PhaseThree()
	{
		CalibrateAccelerometer ();
		canXMove = true;
	}

	void MoveY()
	{
		canYMove = true;
		CalibrateAccelerometer ();
	}

	IEnumerator ResetPlayerCrashed()
	{
		yield return new WaitForSeconds(1.5f);
		StartCoroutine (ResetPlayer());
		yield return new WaitForSeconds(2.5f);
		CalibrateAccelerometer ();
		canXMove = true;
	}

	IEnumerator ResetPlayer()
	{
		yield return new WaitForSeconds(0.5f);
		Vector3 newPosition = transform.position;
		newPosition.x = 0;
		newPosition.y = -4;
		float elapsedTime = 0;
		
		while(elapsedTime < smoothDampTime && transform.position != newPosition)
		{
			transform.position = Vector3.Lerp (transform.position, newPosition, elapsedTime / smoothDampTime);
			elapsedTime += Time.deltaTime;
			yield return new WaitForSeconds(.000001f);
		}
	}

	void Blackhole()
	{
		canXMove = false;
		StartCoroutine (ResetPlayerCrashed());
	}
}
