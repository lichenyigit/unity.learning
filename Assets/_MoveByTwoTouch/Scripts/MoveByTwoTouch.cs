using UnityEngine;

/// <summary>
///	 两个手指控制模型移动
/// </summary>
public class MoveByTwoTouch : MonoBehaviour
{

	private Touch touch0;
	private Touch touch1;

	private Vector3 originalPosition;
	private Vector3 positionDValue;
	
	private void Update()
	{
		if (Input.touchCount != 2)
			return;
		touch0 = Input.GetTouch(0);
		switch (touch0.phase)
		{
			case TouchPhase.Began:
				originalPosition = touch0.position;
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Ended:
				break;
		}
	}

}
