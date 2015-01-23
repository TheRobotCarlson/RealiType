using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider))]

public class NewBehaviourScript : MonoBehaviour 
{

	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curScreenPoint;
	private Vector3 curPosition;
	private bool isPressed;
	private float mouseZ;
	private float transformZ;
	
	protected virtual void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I)) 
						isPressed = true;
		if (Input.GetKeyDown (KeyCode.O))
						isPressed = false;
	}

	void OnMouseDown() 
	{
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		mouseZ = Input.mousePosition.y;

	}
	
	void OnMouseDrag()
	{
		transformZ =(float) (.1*(Input.mousePosition.y - mouseZ));
		if (isPressed) 
		{
			curScreenPoint = new Vector3 (screenPoint.x, screenPoint.y,
			                             screenPoint.z + transformZ );
		}
		else
			curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
		transform.position = curPosition;
	}
}
