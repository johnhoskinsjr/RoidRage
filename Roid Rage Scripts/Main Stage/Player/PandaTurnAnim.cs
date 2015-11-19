/*
 * This script manages the panda turn animation
 * through an observer method called "Turn"
 */ 

using UnityEngine;
using System.Collections;

public class PandaTurnAnim : MonoBehaviour 
{

	public Sprite	pandaCenter;
	public Sprite	pandaLeft;
	public Sprite	pandaRight;

	public GameObject	jetpackParticle;
	public Vector3		jetpackRight;
	public Vector3		jetpackLeft;
	public Vector3		jetpackCenter;

	public SpriteRenderer	pandaRend;

	bool		turnRight = true;

	// Use this for initialization
	void Start() 
	{
		NotificationCentre.AddObserver (this, "Turn");
	}

	/*
	 * This method is an OBSERVER METHOD!!!!
	 */ 
	public void Turn()
	{
		Vector3 jetpackPos = jetpackParticle.transform.localPosition;
		turnRight = !turnRight;
		pandaRend.sprite = pandaCenter;
		jetpackPos = jetpackCenter;
		jetpackParticle.transform.localPosition = jetpackPos;
		StartCoroutine (TurnSprite());
	}

	/*
	 * Plays anaimation of panda turning
	 */ 
	IEnumerator	TurnSprite()
	{
		Vector3 jetpackPos = jetpackParticle.transform.localPosition;
		yield return new WaitForSeconds(0.1f);
		if(turnRight)
		{
			pandaRend.sprite = pandaRight;
			jetpackPos = jetpackRight;
			jetpackParticle.transform.localPosition = jetpackPos;
		}
		else
		{
			pandaRend.sprite = pandaLeft;
			jetpackPos = jetpackLeft;
			jetpackParticle.transform.localPosition = jetpackPos;
		}
	}
}
