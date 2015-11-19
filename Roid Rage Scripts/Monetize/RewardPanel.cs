/*
 * This script manages what happens with rewarded videos
 * What rewards you receive
 * and it unlocks rewards
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class RewardPanel : MonoBehaviour 
{
	public GameObject	pandaReward;
	public GameObject	juggReward;
	public GameObject	demonReward;
	public GameObject	starReward;

	private int			rewardPicked;

	public int			i = 1;

	// Use this for initialization
	void Start () 
	{
		PickReward ();

	}

	void PickReward()
	{
		rewardPicked = Mathf.FloorToInt(Random.Range (0, 4));
		SetRewardPanel();
	}

	void SetRewardPanel ()
	{
		switch(rewardPicked)
		{
		case 0:

			if(SingletonSaveData.instance.PandaLocked && !SingletonSaveData.instance.PandaTrailActive)
			{
				SingletonSaveData.instance.PandaTrailActive = true;
				UnityAnalytics.CustomEvent("Rewards", new Dictionary<string, object>{
					{ "pandaTrial",  i}
				});
				PandaReward();
			}
			else
			{
				StarReward ();
			}
			break;

		case 1:
			if(SingletonSaveData.instance.JuggLocked && !SingletonSaveData.instance.JuggTrailActive)
			{
				SingletonSaveData.instance.JuggTrailActive = true;
				UnityAnalytics.CustomEvent("Rewards", new Dictionary<string, object>{
					{ "juggTrial",  i}
				});
				JuggReward();
			}
			else
			{
				StarReward ();
			}
			break;
		case 2:
			if(SingletonSaveData.instance.DemonLocked && !SingletonSaveData.instance.DemonTrailActive)
			{
				SingletonSaveData.instance.DemonTrailActive = true;
				UnityAnalytics.CustomEvent("Rewards", new Dictionary<string, object>{
					{ "demonTrial",  i}
				});
				DemonReward();
			}
			else
			{
				StarReward ();
			}
			break;
		default:
			StarReward ();
			break;
		}
	}

	void StarReward()
	{
		starReward.SetActive (true);
		SingletonSaveData.instance.TotalStarsCollected = 5;
	}

	void PandaReward()
	{
		pandaReward.SetActive (true);
	}

	void JuggReward()
	{
		juggReward.SetActive (true);
	}

	void DemonReward()
	{
		demonReward.SetActive (true);
	}
}
