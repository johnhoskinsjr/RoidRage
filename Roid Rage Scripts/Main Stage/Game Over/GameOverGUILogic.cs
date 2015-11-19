using UnityEngine;
using System.Collections;

public class GameOverGUILogic : MonoBehaviour 
{
	public GameObject		panelButtonToPauseGame;
	public GameObject		gameOverPanel;
	public GameObject		gameoverGraphics;
	public GameObject		spaceObjects;

	void Start()
	{
		NotificationCentre.AddObserver (this, "GameOver");
		NotificationCentre.AddObserver (this, "AsteroidCrash");
		NotificationCentre.AddObserver (this, "BlackHole");
	}

	/*
	 *	!!!! WARNING OBSERVER METHOD !!!!
	 *	 !! DO NOT CHANGE METHOD NAME !!
	 *
	 *	This method is called when user dies.
	 *	This method controls GUI elements for
	 *	game over panel
	 */
	void GameOver()
	{
		panelButtonToPauseGame.SetActive (false);
		StartCoroutine (GameOverWait());
	}
	
	/*
	 * 	This coroutine purpose is just to freeze loading of
	 * 	gameover panel to allow time for the explosion 
	 * 	animation to be seen by user
	 */
	IEnumerator GameOverWait()
	{
		yield return new WaitForSeconds(2f);
		spaceObjects.SetActive (false);
		gameOverPanel.SetActive (true);
		gameoverGraphics.SetActive (true);
		NotificationCentre.PostNotification (this, "GameOverStarAnimation");
	}
}
