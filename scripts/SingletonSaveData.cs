/*
 * This script manages all the important save data that needs
 * to persist between scene. this data includes:
 * 
 * InstructionsCompleted - whether player has previous completed intructions
 * TotalStarsCollected - How many total stars the player currently has
 * Highscore - Manages the players current highscore
 * PandaLocked - whether the player has unlocked panda character
 * JuggUnlocked - manages whether the player has unlocked the Jugg character
 * DemonUnlocked - whether the player has unlocked the demon character
 * 
 * This script also handles the saving and loading of player data.
 */

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;


public class SingletonSaveData : MonoBehaviour 
{
	private static SingletonSaveData _instance;

	public bool		instructionsCompleted;
	public int		totalStarsCollected;
	public int		highscore;
	public bool		pandaLocked;
	public bool		juggLocked;
	public bool		demonLocked;
	public int		juggCrashes;

	public bool		pandaTrailActive;
	public bool 	juggTrailActive;
	public bool		demonTrailActive;

	public int		i = 1;

	[SerializeField] Texture2D	icon;
	
	private const string 	UNLOCK_PANDA 				= 	"CgkImaKMrMQBEAIQAg";
	private const string	UNLOCK_LUGG 				= 	"CgkImaKMrMQBEAIQAw";
	private const string 	UNLOCK_SPEEDY 				= 	"CgkImaKMrMQBEAIQBA";
	private const string 	TREASURE_HUNTER 			= 	"CgkImaKMrMQBEAIQCg";
	private const string 	GOOD_TREASURE_HUNTER 		= 	"CgkImaKMrMQBEAIQCw";
	private const string 	PRO_TREASURE_HUNTER 		= 	"CgkImaKMrMQBEAIQDA";
	private const string 	LENGEDARY_TREASURE_HUNTER 	= 	"CgkImaKMrMQBEAIQDQ";
	private const string 	RAGER 						= 	"CgkImaKMrMQBEAIQDg";
	private const string 	WEEKEND_RAGER 				= 	"CgkImaKMrMQBEAIQDw";
	private const string 	WEEKDAY_RAGER 				= 	"CgkImaKMrMQBEAIQEA";
	private const string 	LENGENDARY_RAGER 			= 	"CgkImaKMrMQBEAIQEQ";

	private const string	LEADERBOARD_PUBLIC_HIGHSCORE 	= "CgkImaKMrMQBEAIQCA";

	private const string 	PANDA		= "panda";
	private const string  	LUGG		= "lugg";
	private const string	SPEEDY		= "demon";

	public bool PandaTrailActive {
		get 
		{
			return pandaTrailActive;
		}
		set 
		{
			Save ();
			pandaTrailActive = value;
		}
	}

	public bool JuggTrailActive 
	{
		get 
		{
			return juggTrailActive;
		}
		set 
		{
			Save ();
			juggTrailActive = value;
		}
	}

	public bool DemonTrailActive 
	{
		get 
		{
			return demonTrailActive;
		}
		set 
		{
			Save ();
			demonTrailActive = value;
		}
	}

	/*
	 * This property chandles the count of how many times
	 * the jugg character crashes into the asteroids
	 */ 
	public int JuggCrashes
	{
		get
		{
			return juggCrashes;
		}
		set
		{
			juggCrashes = value;
			if(juggCrashes > 2)
			{
				juggCrashes = 0;
			}
		}
	}

	/*
	 * This property manages whether the panda character is unlocked or not.
	 * it also subtracts stars from total star count when character is unlocked.
	 */ 
	public bool PandaLocked
	{
		get
		{
			GPAchievement achievement = GooglePlayManager.instance.GetAchievement(UNLOCK_PANDA);
			if(achievement.state.ToString() == "STATE_UNLOCKED" && pandaLocked || AndroidInAppPurchaseManager.instance.inventory.IsProductPurchased(PANDA))
			{
				GooglePlayManager.instance.UnlockAchievementById(UNLOCK_PANDA);
				pandaLocked = false;
			}
			return pandaLocked;
		}
		set
		{
			GooglePlayManager.instance.UnlockAchievementById(UNLOCK_PANDA);
			totalStarsCollected -= 100;
			pandaLocked = value;
			Save ();
		}
	}

	/*
	 * This property manages whether jugg character is unlocked or not.
	 * It also subtracts the stars when character is unlocked.
	 */ 
	public bool JuggLocked
	{
		get
		{
			GPAchievement achievement = GooglePlayManager.instance.GetAchievement(UNLOCK_LUGG);
			if(achievement.state.ToString() == "STATE_UNLOCKED" && juggLocked || AndroidInAppPurchaseManager.instance.inventory.IsProductPurchased(LUGG))
			{
				GooglePlayManager.instance.UnlockAchievementById(UNLOCK_LUGG);
				juggLocked = false;
			}
			return juggLocked;
		}
		set
		{
			GooglePlayManager.instance.UnlockAchievementById(UNLOCK_LUGG);
			totalStarsCollected -= 250;
			juggLocked = value;
			Save ();
		}
	}

	/*
	 * This property manages whether the demon character is unlocked or not.
	 * It also subtracts stars when character is unlocked
	 */ 
	public bool DemonLocked
	{
		get
		{
			GPAchievement achievement = GooglePlayManager.instance.GetAchievement(UNLOCK_SPEEDY);
			if(achievement.state.ToString() == "STATE_UNLOCKED" && demonLocked || AndroidInAppPurchaseManager.instance.inventory.IsProductPurchased(SPEEDY))
			{
				GooglePlayManager.instance.UnlockAchievementById(UNLOCK_SPEEDY);
				demonLocked = false;
			}
			return demonLocked;
		}
		set
		{
			GooglePlayManager.instance.UnlockAchievementById(UNLOCK_SPEEDY);
			totalStarsCollected -= 300;
			demonLocked = value;
			Save ();
		}
	}

	/*
	 * This property checks whether current score is higher than highscore, and sets
	 * it accordingly.
	 */ 
	public int Highscore
	{
		get
		{
			return highscore;
		}
		set
		{
			if(value > highscore)
			{
				if(value >= 100)
				{
					GooglePlayManager.instance.UnlockAchievementById(RAGER);
					if(value >= 500)
					{
						GooglePlayManager.instance.UnlockAchievementById(WEEKEND_RAGER);
						if(value >= 1000)
						{
							GooglePlayManager.instance.UnlockAchievementById(WEEKDAY_RAGER);
							if(value >= 1500)
							{
								GooglePlayManager.instance.UnlockAchievementById(LENGENDARY_RAGER);
							}
						}
					}
				}
				GooglePlayManager.instance.SubmitScoreById(LEADERBOARD_PUBLIC_HIGHSCORE, value);
				UnityAnalytics.CustomEvent("Score", new Dictionary<string, object>{
					{ "averageHighscore",  value},
					{ "timesHighscoreReached", i }
				});
				highscore = value;
				Save ();
			}
		}
	}

	/*
	 * This property manages whether the player has previously completed 
	 * instructions or not
	 */ 
	public bool InstructionsCompleted
	{
		get
		{
			return instructionsCompleted;
		}
		set
		{
			instructionsCompleted = value;
			Save ();
		}
	}

	/*
	 * This property manages the total collected stars.
	 * It adds the sent value to the total count. It also
	 * sets limits on mix and min number of stars.
	 */ 
	public int TotalStarsCollected
	{
		get
		{
			if(totalStarsCollected > 999)
			{
				totalStarsCollected = 999;
			}
			if(totalStarsCollected < 0)
			{
				totalStarsCollected = 0;
			}
			return totalStarsCollected;
		}
		set
		{
			GooglePlayManager.instance.IncrementAchievementById (TREASURE_HUNTER, value);
			GooglePlayManager.instance.IncrementAchievementById (GOOD_TREASURE_HUNTER, value);
			GooglePlayManager.instance.IncrementAchievementById (PRO_TREASURE_HUNTER, value);
			GooglePlayManager.instance.IncrementAchievementById (LENGEDARY_TREASURE_HUNTER, value);
			totalStarsCollected += value;
			if(totalStarsCollected > 999)
			{
				totalStarsCollected = 999;
			}
			if(totalStarsCollected < 0)
			{
				totalStarsCollected = 0;
			}
			Save ();
		}
	}

	/*
	 * Property that manages the singleton class instance
	 */ 
	public static SingletonSaveData instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SingletonSaveData>();
				
				//Tell unity not to destroy this object when loading a new scene!
				//DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	/*
	 * When gameobject is created checks to make sure there isnt 
	 * an instance of singleton class before creating one, 
	 * and setting it to not destroy on load.
	 */ 
	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}

	/*
	 * Loads all the players saved info
	 */ 
	void Start()
	{
		juggCrashes = 0;
		Load ();
	}

	/*
	 * This method is called every time one of the properties are set.
	 * It sets all data from this class to equal data from serializable class.
	 * Saves data to local hard drive.
	 */ 
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.instructionsCompleted = instructionsCompleted;
		data.totalStarsCollected = totalStarsCollected;
		data.highscore = highscore;
		data.pandaLocked = pandaLocked;
		data.juggLocked = juggLocked;
		data.demonLocked = demonLocked;
		data.pandaTrailActive = pandaTrailActive;
		data.juggTrailActive = juggTrailActive;
		data.demonTrailActive = demonTrailActive;

		bf.Serialize (file, data);
		file.Close ();
	
	}


	/*
	 * Checks to see if save data already exist. If it does then load it,
	 * else initialize all data to default values.
	 */ 
	public void Load()
	{
		if(File.Exists (Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close();

			instructionsCompleted = data.instructionsCompleted;
			totalStarsCollected = data.totalStarsCollected;
			highscore = data.highscore;
			pandaLocked = data.pandaLocked;
			juggLocked = data.juggLocked;
			demonLocked = data.demonLocked;
			pandaTrailActive = data.pandaTrailActive;
			juggTrailActive = data.juggTrailActive;
			demonTrailActive = data.demonTrailActive;
		}
		else
		{
			instructionsCompleted = false;
			totalStarsCollected = 0;
			highscore = 0;
			pandaLocked = true;
			juggLocked = true;
			demonLocked = true;
			pandaTrailActive = false;
			juggTrailActive = false;
			demonTrailActive = false;
		}
	}
}

/*
 * Serializable class for the saving and loading of played data
 */ 
[Serializable]
class PlayerData
{
	public bool instructionsCompleted;
	public int	totalStarsCollected;
	public int 	highscore;
	public bool pandaLocked;
	public bool juggLocked;
	public bool demonLocked;
	public bool	pandaTrailActive;
	public bool	juggTrailActive;
	public bool	demonTrailActive;
}