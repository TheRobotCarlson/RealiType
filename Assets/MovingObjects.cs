using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider),typeof (SphereCollider))]

public class MovingObjects : MonoBehaviour 
{
	
	private Vector3 screenPoint;
	private Vector3 curScreenPoint;
	private Vector3 curPosition;
	private bool isPressed = false;
	private float mouseZ;
	private float transformZ;
	private int indexNumMatch;
	private Vector3[] coordinates;
	
	protected virtual void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I)) 
			isPressed = true;
		if (Input.GetKeyDown (KeyCode.O))
			isPressed = false;
	}
	
	void OnMouseDown()
	{
		
		Vector3 click = gameObject.transform.position;
		screenPoint = Camera.main.WorldToScreenPoint (click);
		mouseZ = Input.mousePosition.y;	
		GameObject[] objects =  UnityEngine.Object.FindObjectsOfType<GameObject>();
		int size = objects.Length;
		coordinates = new Vector3[size];
		for (int item = 0; item < size; item ++)
		{
			if (click == objects[item].transform.position)
			{
				indexNumMatch = item;
			}
			coordinates[item] = objects [item].transform.position;
		}


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
		bool matches = false;
		for (int i = 0; i < coordinates.Length; i++) 
		{
			if(i != indexNumMatch && Mathf.Abs(coordinates[i].x - curPosition.x) <= 1 &&
			    Mathf.Abs(coordinates[i].y - curPosition.y) <= 1 &&
			    Mathf.Abs(coordinates[i].z - curPosition.z) <= 1)
				matches = true;
		}
		if (curPosition.y >= 0 && matches == false)
			transform.position = curPosition;
	}


}
