/*
 * This script handles all the monolog, and post some notifications.
 * It controls when events happen. This script is mostly time based.
 */ 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIInstructionsController : MonoBehaviour 
{
	Text					scriptText; // The Text component that holds the text used to display script.
	List<GameObject>		guiList = new List<GameObject>(); // The gameobject of the text coponent.
	public static bool		isPaused = false; // Holds paused state.
	int						phase = 0; // Holds the phase of instructions. 0 = phase one; 1 = phase two; 2 = phase three
	int						i = 0; // used for switch. Resets after each phase.
	float					t= 0; // Holds the timer for each case. Resets after each case.
	int 					crash = 0; // Holds number of crashes into asteroids
	int 					suckedByBlackhole = 0; // Holds number of times entered into blackhole.

	private const string 	GETTING_ANGRY 	= 	"CgkImaKMrMQBEAIQAQ";

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "PhaseDone");
		NotificationCentre.AddObserver (this, "PhaseThreeDone");
		NotificationCentre.AddObserver (this, "Crashed");
		NotificationCentre.AddObserver (this, "Blackhole");
		NotificationCentre.AddObserver (this, "Pause");

		// Sets text gameobject to variable.
		foreach(Transform child in transform)
		{
			guiList.Add (child.gameObject);
		}

		// Sets text component to a variable.
		scriptText = guiList[0].GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	
	{
		// Checks for paused state.
		if(isPaused) return;

		// This is the timer.
		t += Time.deltaTime;
		
		switch(phase)
		{
		/*
		 * PHASE ONE
		 */ 
		case 0:

			switch(i)
			{
			case 0:
				if(t >= 1.0f) 
					RunNextCase ();
				break;
			case 1:
				guiList[0].SetActive (true);
				scriptText.text = "Okay.";
				if(t >= 1.0f) 
					RunNextCase ();
				break;
			case 2:
				scriptText.text = "See this little guy here?";
				if(t >= 3.0f) 
				{
					RunNextCase ();
					NotificationCentre.PostNotification (this, "PhaseOne");
				}
				break;
			case 3:
				scriptText.text = "He always bounces left and right.";
				if(t >= 5.0f) 
					RunNextCase ();
				break;
			case 4:
				scriptText.text = "There's nothing you can do about that.";
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 5:
				scriptText.text = "You can't tilt the phone left.";
				if(t >= 2.0f) 
					RunNextCase ();
				break;
			case 6:
				scriptText.text = "You can't tilt the phone right.";
				if(t >= 2.0f) 
					RunNextCase ();
				break;
			case 7:
				scriptText.text = "It doesn't matter.";
				if(t >= 2.5f) 
					RunNextCase ();
				break;
			case 8:
				scriptText.text = "But...";
				if(t >= 2.5f) 
					RunNextCase ();
					NotificationCentre.PostNotification (this, "MoveY");
				break;
			case 9:
				scriptText.text = "You can tilt the phone up and down!";
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 10:
				scriptText.text = "Try It Out!";
				if(t >= 1.5f) 
				{
					RunNextCase ();
				}
				break;
			case 11:
				scriptText.text = "";
				guiList[0].SetActive (false);
				if(t >= 5.0f) 
					RunNextCase ();
				break;
			case 12:
				scriptText.text = "Nice!";
				guiList[0].SetActive (true);
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 13:
				scriptText.text = "Let's have a little fun.";
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 14:
				scriptText.text = "See if you can collect these stars.";
				if(t >= 3.0f) 
				{
					RunNextCase ();
				}
				break;
			case 15:
				scriptText.text = "";
				guiList[0].SetActive (false);
				if(t >= 0.1f) 
				{
					NotificationCentre.PostNotification (this, "ReadyForStars");
					RunNextCase ();
				}
				break;
			}
			break; // phase 1 case break

		/*
		 * PHASE TWO
		 */ 
		case 1:
			switch(i)
			{
			case 0:
				guiList[0].SetActive (true);
				scriptText.text = "Good job!";
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 1:
				scriptText.text = "...I guess";
				if(t >= 1.5f) 
					RunNextCase ();
				break;
			case 2:
				scriptText.text = "I think we can make this a little harder.";
				if(t >= 2.5f) 
				{
					RunNextCase ();
				}
				break;
			case 3:
				scriptText.text = "See if you can get the star now.";
				if(t >= 2.5f) 
				{
					RunNextCase ();
				}
				break;
			case 4:
				scriptText.text = "";
				guiList[0].SetActive (false);
				if(t >= 0.5f) 
				{
					NotificationCentre.PostNotification (this, "PhaseTwo");
					NotificationCentre.PostNotification (this, "ReadyForStars");
					RunNextCase ();
				}
				break;	
			}
			break; // Phase 2 case break

		/*
		 * PHASE THREE
		 */ 
		case 2:
			switch(i)
			{
			case 0:
				guiList[0].SetActive (true);
				if(crash == 0)
				{
					scriptText.text = "Nice! First Try!";
				}
				else if(crash <= 5)
				{
					scriptText.text = "Not bad. Only needed " + (crash + 1) + " tries.";
				}
				else if(crash > 5)
				{
					scriptText.text = "Wow! this must be a record. " + (crash + 1) + " tries.";
				}
				if(t >= 3.0f) 
					RunNextCase ();
				break;

			case 1:
				scriptText.text = "One more suprise.";
				if(t >= 3.0f) 
				{
					NotificationCentre.PostNotification (this, "StartBlackhole");
					RunNextCase ();
				}
				break;

			case 2:
				scriptText.text = "The blackhole...";
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 3:
				scriptText.text = "If you hit it,";
				if(t >= 3.0f) 
					RunNextCase ();
				break;
			case 4:
				scriptText.text = "you will lose your stars!";
				if(t >= 4.0f) 
					RunNextCase ();
				break;
			case 5:
				scriptText.text = "so don't hit it!";
				if(t >= 2.0f) 
					RunNextCase ();
				break;

			case 6:
				scriptText.text = "";
				guiList[0].SetActive (false);
				if(t >= 0.5f) 
				{
					NotificationCentre.PostNotification (this, "ReadyForStars");
					NotificationCentre.PostNotification (this, "PhaseThree");
					RunNextCase ();
				}
				break;
			}
			break; // Phase 3 break

		/*
		 * END OF INSTRUCTIONS
		 */ 
		case 3:
			switch(i)
			{
			case 0:
				guiList[0].SetActive (true);
				scriptText.text = "see that wasn't so bad.";
				if(t >= 1.5f) 
				{	
					if(SingletonSaveData.instance.InstructionsCompleted)
					{
						i = 3;
					}
					RunNextCase ();
				}
				break;

			case 1:
				scriptText.text = "For being so nice about it,";
				if(t >= 1.5f) 
					RunNextCase ();
				break;

			case 2:
				scriptText.text = "For being so nice about it,";
				if(t >= 1.5f) 
				{
					RunNextCase ();
					NotificationCentre.PostNotification (this, "ReadyForStars");
					SingletonSaveData.instance.TotalStarsCollected = 10;
					SingletonSaveData.instance.InstructionsCompleted = true;
					GooglePlayManager.instance.UnlockAchievementById(GETTING_ANGRY);
				}
				break;

			case 3:
				scriptText.text = "I'm going to give you 10 stars!";
				if(t >= 2.0f) 
					RunNextCase ();
				break;

			case 4:
				scriptText.text = "Now let's play some roid rage!";
				if(t >= 3.0f) 
					Application.LoadLevel ("Character_Select");
				break;
			}
			break;
	
		}
	}

	/*
	 * Method called after every case. It resets variables
	 */ 
	void RunNextCase()
	{
		t = 0;
		i++;
	}

	/*
	 * OBSERVER METHOD
	 */ 
	void PhaseDone()
	{
		phase++;
		i = 0;
		t = 0;
	}

	/*
	 * OBSERVER METHOD
	 */ 
	void Pause()
	{
		isPaused = !isPaused;
	}

	/*
	 * OBSERVER METHOD
	 */ 
	void Crashed()
	{
		StartCoroutine (AsteroidScript());
	}

	/*
	 * Coroutine used to display asteroid monolog
	 */ 
	IEnumerator AsteroidScript()
	{
		crash++;
		guiList[0].SetActive (true);
		switch(crash)
		{
		case 1:	
			yield return new WaitForSeconds(1.0f);

			scriptText.text = "Okay.";
			yield return new WaitForSeconds(1.5f);
			scriptText.text = "Don't hit those.";
			yield return new WaitForSeconds(2.0f);

			break;
		case 2:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "What did I say?";
			yield return new WaitForSeconds(3.0f);
			break;

		case 4:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "Seriously?";
			yield return new WaitForSeconds(3.0f);
			break;

		case 5:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "I think you're just playing around.";
			yield return new WaitForSeconds(3.0f);
			break;

		default:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "Try again, It's not that hard!";
			yield return new WaitForSeconds(3.0f);
			break;
		}

		scriptText.text = "";
		guiList[0].SetActive (false);
	}

	/*
	 * OBSERVER METHOD
	 */ 
	void Blackhole()
	{
		StartCoroutine (BlackholeScript());
	}

	/*
	 * Coroutine used to display blackhole script
	 */ 
	IEnumerator BlackholeScript()
	{
		suckedByBlackhole++;
		guiList[0].SetActive (true);
		switch(suckedByBlackhole)
		{
		case 1:	
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "you lose everything when you hit them.";
			yield return new WaitForSeconds(3.0f);
			
			break;
			
		case 4:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "Seriously?";
			yield return new WaitForSeconds(3.0f);
			break;
			
		case 5:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "I think you're just playing around.";
			yield return new WaitForSeconds(3.0f);
			break;
			
		default:
			yield return new WaitForSeconds(1.0f);
			scriptText.text = "Try again, It's not that hard!";
			yield return new WaitForSeconds(3.0f);
			break;
		}
		
		scriptText.text = "";
		guiList[0].SetActive (false);
	}
}
