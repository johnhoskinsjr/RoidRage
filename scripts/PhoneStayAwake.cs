/*
 * This scripts only job is to make sure phone never goes to sleep
 */

using UnityEngine;
using System.Collections;

public class PhoneStayAwake : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
	}
}
