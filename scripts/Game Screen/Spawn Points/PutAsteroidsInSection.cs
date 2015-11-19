using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PutAsteroidsInSection : MonoBehaviour 
{
	public GameObject 			asteroid_1;
	public GameObject 			asteroid_2;
	public GameObject 			asteroid_3;
	public GameObject 			asteroid_4;
	
	/*
	 * Checks to see if the black hole is ready to spawn
	 */ 
	void OnEnable()
	{
		/*
		 * This foreach loops checks to see if there is already
		 * a black hole game object, and if there is set inactive
		 */ 
		foreach(Transform child in transform)
		{
			if(child.name == "Asteroid")
			{
				foreach(Transform newChild in child)
				{

					if(newChild != null)
					{
						return;
					}
				}

				CreateSection(child);
			}
		}
	}

	void CreateSection (Transform child)
	{
		GameObject asteroid;

		if(child.tag == "1")
		{
			asteroid = Instantiate (asteroid_1) as GameObject;
		}
		else if(child.tag == "2")
		{
			asteroid = Instantiate (asteroid_2) as GameObject;
		}
		else if(child.tag == "3")
		{
			asteroid = Instantiate (asteroid_3) as GameObject;
		}
		else
		{
			asteroid = Instantiate (asteroid_4) as GameObject;
		}

		asteroid.transform.parent = child;
		asteroid.transform.localPosition = Vector3.zero;
	}
	
	/*
	 * This is an OBSERVER METHOD
	 */ 
	void GameOver()
	{
		Destroy (this);
	}
}

