using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (LoadNextLevel());
	}

	IEnumerator LoadNextLevel()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("Title");
	}
}
