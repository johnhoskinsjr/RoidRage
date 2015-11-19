using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour 
{
	public Text	starCounterText;	

	void Update()
	{
		starCounterText.text = "" + SingletonSaveData.instance.TotalStarsCollected;
	}
}
