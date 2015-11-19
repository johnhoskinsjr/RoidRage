using UnityEngine;
using System.Collections;

public class SpaceObjectsAssets : MonoBehaviour 
{
	private static GameObject 	blackhole;
	private static GameObject 	star;
	private static GameObject	asteroid1;
	private static GameObject	asteroid2;
	private static GameObject	asteroid3;
	private static GameObject	asteroid4;

	void Start()
	{
		blackhole = Resources.Load ("Black Hole")as GameObject;
		star = Resources.Load ("Star") as GameObject;
		asteroid1 = Instantiate (Resources.Load ("Asteroid_1")) as GameObject;
		asteroid2 = Instantiate (Resources.Load ("Asteroid_1")) as GameObject;
		asteroid3 = Instantiate (Resources.Load ("Asteroid_1")) as GameObject;
		asteroid4 = Instantiate (Resources.Load ("Asteroid_1")) as GameObject;
	}

	public static GameObject GetBlackHole()
	{
		return blackhole;
	}

	public static GameObject GetStar()
	{
		return star;
	}

	public static GameObject GetAsteroidOne()
	{
		return asteroid1;
	}

	public static GameObject GetAsteroidTwo()
	{
		return asteroid2;
	}

	public static GameObject GetAsteroidThree()
	{
		return asteroid3;
	}

	public static GameObject GetAsteroidFour()
	{
		return asteroid4;
	}
}
