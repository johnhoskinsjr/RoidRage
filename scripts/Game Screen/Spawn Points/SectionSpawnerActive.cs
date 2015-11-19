using UnityEngine;
using System.Collections;

public class SectionSpawnerActive : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "StartGame");
	}

	void StartGame()
	{
		foreach(Transform child in transform)
		{
			child.gameObject.SetActive (true);
		}
	}
}
