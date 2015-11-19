using UnityEngine;
using System.Collections;

public class GooglePlaySetup : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "BlackHole");

		if(GooglePlayConnection.state != GPConnectionState.STATE_CONNECTED) 
		{
			GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived;
			LoadAchievements();
		}
	}

	void LoadAchievements ()
	{
		GooglePlayConnection.instance.connect ();
	}

	private void ActionConnectionResultReceived(GooglePlayConnectionResult result) 
	{
		if(result.IsSuccess) 
		{
			GooglePlayManager.ActionAchievementsLoaded +=  OnAchievmentsLoaded; 
			GooglePlayManager.ActionLeaderboardsLoaded += OnLeaderBoardsLoaded;
			GooglePlayManager.instance.LoadLeaderBoards ();
			GooglePlayManager.instance.LoadAchievements();
		} 
		else
		{

		}
	}

	private void OnLeaderBoardsLoaded(GooglePlayResult result) 
	{
		GooglePlayManager.ActionLeaderboardsLoaded -= OnLeaderBoardsLoaded;
		if(result.isSuccess) 
		{
			if( GooglePlayManager.instance.GetLeaderBoard("CgkImaKMrMQBEAIQCA") == null) 
			{

			}
		} 
		else 
		{

		}
	}

	private void OnAchievmentsLoaded(GooglePlayResult result) 
	{
		GooglePlayManager.ActionAchievementsLoaded -=  OnAchievmentsLoaded; 
		if(result.isSuccess) 
		{
			foreach(string achievementId in GooglePlayManager.instance.achievements.Keys) 
			{
				GPAchievement achievement = GooglePlayManager.instance.GetAchievement(achievementId);
				Debug.Log(achievement.id);
				Debug.Log(achievement.name);
				Debug.Log(achievement.description);
				Debug.Log(achievement.type);
				Debug.Log(achievement.state);
				Debug.Log(achievement.currentSteps);
				Debug.Log(achievement.totalSteps);
			}
		} 
		else 
		{

		}
	}

	public void LoadAchievementList()
	{
		GooglePlayManager.instance.ShowAchievementsUI ();
	}

	public void LoadLeaderboards()
	{
		GooglePlayManager.instance.ShowLeaderBoardById ("CgkImaKMrMQBEAIQCA");
	}
}
