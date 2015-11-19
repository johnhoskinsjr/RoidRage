/* 	This scripts only job is to make sure
 * 	the time scale of the game is at 1
 * 	when scene starts
 */ 

using UnityEngine;
using System.Collections;

public class Unpause : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1;
	}
}
