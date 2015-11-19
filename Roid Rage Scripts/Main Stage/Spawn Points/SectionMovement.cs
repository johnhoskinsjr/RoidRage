using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectionMovement : MonoBehaviour {

	public float	destroyTime;
	
	void Start()
	{
		NotificationCentre.AddObserver (this, "GameOver");
	}

	// Use this for initialization
	void OnEnable () 
	{
		Vector3 sectionPos = transform.position;
		sectionPos.y = 9;
		transform.position = sectionPos;
		destroyTime = WallSpawnController.GetWaitTime () * 2;
		StartCoroutine (Destroy());
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 sectionPos = gameObject.transform.position;
		sectionPos.y -= WallSpawnController.GetSectionSpeed () * Time.deltaTime;
		transform.position = sectionPos;
	}
	
	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(destroyTime);
		gameObject.SetActive (false);
	}
	
	void GameOver()
	{
		Destroy (this);
	}
}
