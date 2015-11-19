using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		const string projectId = "51edbe7b-2a3f-4921-b2d4-79e826c09c52";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}