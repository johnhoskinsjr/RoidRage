/*
 * This script changes the crash site sprite depending
 * on current player selected
 */ 

using UnityEngine;
using System.Collections;

public class CrashSiteImages : MonoBehaviour 
{
	public Sprite		johnnyGreen;
	public Sprite		aurora;
	public Sprite		panda;
	public Sprite		jugg;
	public Sprite		demon;

	public GameObject	johnnyGreenExtras;
	public GameObject	auroraExtras;
	public GameObject	artziLosoExtras;
	public GameObject	luggExtras;
	public GameObject	speedyExtras;

	public SpriteRenderer	image;

	// Use this for initialization
	void OnEnable () 
	{
		int characterSelected = PlayerPrefs.GetInt ("SelectedCharacter");

		switch(characterSelected)
		{
		case 0:
			image.sprite = johnnyGreen;
			johnnyGreenExtras.SetActive (true);
			speedyExtras.SetActive (false);
			break;

		case 1:
			image.sprite = aurora;
			auroraExtras.SetActive (true);
			speedyExtras.SetActive (false);
			break;

		case 2:
			image.sprite = panda;
			artziLosoExtras.SetActive (true);
			speedyExtras.SetActive (false);
			break;

		case 3:
			image.sprite = jugg;
			luggExtras.SetActive (true);
			speedyExtras.SetActive(false);
			break;

		case 4:
			image.sprite = demon;
			speedyExtras.SetActive (true);
			break;

		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
