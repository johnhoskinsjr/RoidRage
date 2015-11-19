/*
 * 	This scripts only job is to control GUI
 * 	elements of the game screen
 */ 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

	public GameObject	starCounter;
	public GameObject	scoreCounter;
	public GameObject	panelButtonToPauseGame;
	public GameObject	panelButtonToStartGame;
	public GameObject	startText;
	public AudioSource	buttonSound;
	bool 				isGamePaused = false;
	public GameObject	pauseImage;

	/*
	 * 	This method calls method that starts coroutine 
	 * 	that allows time for button sound to play and 
	 * 	reloads game screen
	 */ 
	public void RetryButton()
	{
		RetryButtonCoroutine();
	}

	/*
	 * 	This method controls what happen when the 
	 * 	panel button that starts the game is pressed
	 */ 
	public void PanelButtonToStartGame()
	{
		NotificationCentre.PostNotification (this, "StartGame");
		starCounter.SetActive (true);
		scoreCounter.SetActive (true);
		panelButtonToPauseGame.SetActive (true);
		panelButtonToStartGame.SetActive (false);
		startText.SetActive (false);
	}

	/*
	 * 	This method controls what happens when the
	 * 	panel button that pauses game is pressed
	 */ 
	public void PanelButtonTopauseGame()
	{
		isGamePaused = !isGamePaused;

		if(isGamePaused)
		{
			NotificationCentre.PostNotification (this, "GamePaused");
			pauseImage.SetActive (true);
		}
		else
		{
			NotificationCentre.PostNotification (this, "GameUnpaused");
			pauseImage.SetActive (false);
		}
	}

	/*
	 * 	This method calls method that starts coroutine 
	 * 	that allows time for button sound to play and 
	 * 	loads loading screen
	 */ 
	public void HomeButton()
	{	
		HomeButtonCoroutine();
	}

	/*
	 * 	This method starts coroutine for retry button
	 */
	void RetryButtonCoroutine()
	{
		StartCoroutine (ReloadGameScreen());
	}

	/*
	 * 	This method starts coroutine for home button
	 */
	void HomeButtonCoroutine()
	{
		StartCoroutine (LoadLoadingScreen());
	}

	/*
	 * 	This is the coroutine that controls what happens
	 * 	when retry button is pressed
	 */ 
	IEnumerator	ReloadGameScreen()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		yield return new WaitForSeconds(.3f);
		Application.LoadLevel ("Game");
	}

	/*
	 *	This is the coroutine that controls what happens
	 *	when the home button is pressed
	 */
	IEnumerator	LoadLoadingScreen()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		yield return new WaitForSeconds(.3f);
		Application.LoadLevel ("Title");
	}

	/*
	 * 	Adds observers
	 */
	void Start()
	{
		NotificationCentre.AddObserver (this, "GameOver");
	}

	/*
	 * controls paused and unpaused state
	 */ 
	void Update()
	{
		if(isGamePaused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
