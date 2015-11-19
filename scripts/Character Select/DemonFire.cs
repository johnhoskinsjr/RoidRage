using UnityEngine;
using System.Collections;

public class DemonFire : MonoBehaviour 
{
	[SerializeField] GameObject		fire;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.transform.localScale.x == 0.7f)
		{
			fire.SetActive (true);
		}
		else
		{
			fire.SetActive (false);
		}
	}
}
