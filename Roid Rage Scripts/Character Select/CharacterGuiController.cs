/*
 * This script handles all the GUI functionality for the
 * character select screen.
 */ 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class CharacterGuiController : MonoBehaviour
{
	//Declare Variables
	public AudioSource 	buttonSound; 	//Button SFX

	public GameObject	unlockPanel; 	//Model Panel
	public GameObject	panelTextGO;	//Dynamic Decription
	public GameObject	starButtonTextGO;	//Star Cost Text
	public GameObject	moneyButtonTextGO;	//Dollar Amount Text
	public GameObject	yesButton;	
	public GameObject	noButton;
	public GameObject	starTextGO;
	public GameObject	moneyTextGO;
	public GameObject	notEnoughPanel;	//Text displayed when player can't afford item.

	public static int	characterSelected;// 1 = Panda, 2 = Jugg, 3 = Demon

	public bool			buyingWithStars = false;
	public static bool	buyingWithCash = false;

	public Text			panelText;
	public Text			starText;
	public Text			moneyText;

	public int			i = 0;


	//Method called when script is first initialized.
	public void Start()
	{
		NotificationCentre.AddObserver (this, "Panda");
		NotificationCentre.AddObserver (this, "Jugg");
		NotificationCentre.AddObserver (this, "Demon");
		panelText = panelTextGO.GetComponent<Text>();
		starText = starTextGO.GetComponent<Text>();
		moneyText = moneyTextGO.GetComponent<Text>();
	}

	//What happens when the player presses unlock button for
	public void UnlockButton()
	{
		NotificationCentre.PostNotification (this, "UnlockPanel");
		buttonSound.PlayOneShot (buttonSound.clip);
		unlockPanel.SetActive (true);
		panelText.text = "How would you like to unlock character?";
		noButton.SetActive (false);
		yesButton.SetActive (false);
	}

	//What happens when the player chooses to exit the unlock character panel
	public void ExitPanelButton()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		CloseUnlockPanel ();
	}

	//What happens when the player presses start button
	public void PlayButton()
	{
		PlayerPrefs.Save ();
		buttonSound.PlayOneShot (buttonSound.clip);
		StartCoroutine (WaitForButtonSound());
	}

	//Waits for button sound when the start button is pressed
	IEnumerator WaitForButtonSound()
	{
		yield return new WaitForSeconds(.1f);
		Application.LoadLevel ("Game");

	}

	// Manages the buy with stars button
	public void BuyWithStars()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		buyingWithStars = true;
		buyingWithCash = false;
		ChangeText ();
	}

	//Manages the buy with cash button
	public void BuyWithMoney()
	{
		buttonSound.PlayOneShot (buttonSound.clip);
		buyingWithCash = true;
		buyingWithStars = false;
		StartCoroutine(ClosePanelForPurchase());
	}

	//Changes the text for the panel after payment option selected
	public void ChangeText()
	{
		panelText.text = "Are you sure?";
		yesButton.SetActive (true);
		noButton.SetActive (true);
	}

	//Manages the NO button
	public void NoButton()
	{
		CloseUnlockPanel ();
	}

	//Manages the yes button
	public void YesButton()
	{
		// If player is unlocking with stars run this switch statement
		if(buyingWithStars)
		{
			switch(characterSelected)
			{
				//IF panda is selected character
			case 1:
				if(SingletonSaveData.instance.totalStarsCollected < 100)
				{
					StartCoroutine (NotEnoughStars());
				}
				else
				{
					SingletonSaveData.instance.PandaLocked = false;
					UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
						{ "starBoughtPanda",  i}
					});
				}
				break;

				//If jugg is selcted character
			case 2:
				if(SingletonSaveData.instance.totalStarsCollected < 250)
				{
					StartCoroutine (NotEnoughStars());
				}
				else
				{
					SingletonSaveData.instance.JuggLocked = false;
					UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
						{ "starBoughtLugg",  i}
					});
				}
				break;

				//If demon is selected character
			case 3:
				if(SingletonSaveData.instance.totalStarsCollected < 300)
				{
					StartCoroutine (NotEnoughStars());
				}
				else
				{
					SingletonSaveData.instance.DemonLocked = false;
					UnityAnalytics.CustomEvent("Characters", new Dictionary<string, object>{
						{ "starBoughtDemon",  i}
					});
				}
				break;
			}
			buttonSound.PlayOneShot (buttonSound.clip);

		}
		else if (buyingWithCash)
		{

		}

		CloseUnlockPanel ();
	}

	//Observer method letting GUI system know panda is current selected character
	public void Panda()
	{
		starText.text = "100";
		moneyText.text = "$ 0.99";
		characterSelected = 1;
	}

	//Observer method letting GUI system know jugg is current selected character
	public void Jugg()
	{
		starText.text = "250";
		moneyText.text = "$ 1.99";
		characterSelected = 2;
	}

	//Observer method letting GUI system know demon is current selected character
	public void Demon()
	{
		starText.text = "300";
		moneyText.text = "$ 1.99";
		characterSelected = 3;
	}

	//Method that closes the Unlock Panel
	public void CloseUnlockPanel()
	{
		NotificationCentre.PostNotification (this, "UnlockPanel");
		yesButton.SetActive (false);
		noButton.SetActive (false);
		unlockPanel.SetActive (false);
		buyingWithCash = false;
		buyingWithStars = false;
		characterSelected = 0;
	}

	//Displays text letting player know they didnt have enough stars
	IEnumerator NotEnoughStars()
	{
		yield return new WaitForSeconds(0.5f);
		notEnoughPanel.SetActive (true);
		yield return new WaitForSeconds(1.5f);
		notEnoughPanel.SetActive (false);
	}

	IEnumerator ClosePanelForPurchase()
	{
		NotificationCentre.PostNotification (this, "PurchaseCharacter");
		yield return new WaitForSeconds(0.5f);
		CloseUnlockPanel ();
	}
}
