using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour 
{
	
	public float minSwipeDistY;
	
	public float minSwipeDistX;
	
	private Vector2 startPos;

	public static bool cantGoRight;
	public static bool cantGoLeft;
	public static bool waitingForLerp;

	void Update()
	{
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches[0];

			switch (touch.phase) 
			{
			case TouchPhase.Began:
				startPos = touch.position;
				break;

			case TouchPhase.Ended:
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) 
				{
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					if (swipeValue > 0)//up swipe
					{

					}	
					else if (swipeValue < 0)//down swipe
					{

					}		
				}
				
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				
				if (swipeDistHorizontal > minSwipeDistX) 
				{
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					if (swipeValue > 0)//right swipe
					{
						if(cantGoLeft)
							return;
						if(waitingForLerp)
							return;
						NotificationCentre.PostNotification (this, "SwipeRight");
						waitingForLerp = true;
					}	
					else if (swipeValue < 0)//left swipe
					{
						if(cantGoRight)
							return;
						if(waitingForLerp)
							return;
						NotificationCentre.PostNotification (this, "SwipeLeft");
						waitingForLerp = true;
					}		
				}
				break;
			}
		}
	}
}