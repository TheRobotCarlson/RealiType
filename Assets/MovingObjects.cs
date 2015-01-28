using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider),typeof (SphereCollider))]

public class MovingObjects : MonoBehaviour 
{
	//The point on the screen that the object is at.
    private Vector3 screenPoint;
    //The transformation of screenPoint once the object is moved.
	private Vector3 curScreenPoint;
    //The position of the object in the world based on its position on the screen.
	private Vector3 curPosition;
    //Boolean that'll determine if movement will go in the z-direction.
	private bool isPressed = false;
    //Mouse's "z-coordinate"
	private float mouseZ;
    //Handle the transformation of the object in the z-direction.
	private float transformZ;
    //Number of the clicked object in the index
	private int indexNumMatch;
    //Array of the coordinates of the center of the colliders in the objects.
	private Vector3[] coordinates;
	
    //Self-updating method in the scene.
	protected virtual void Update ()
	{
        //The pressing of the "I" key will activate transformation in the z-direction.
		if (Input.GetKeyDown (KeyCode.I)) 
			isPressed = true;
        //The pressing of the "O" key will deactivate tranformations in the z-direction.
		if (Input.GetKeyDown (KeyCode.O))
			isPressed = false;
	}
	
    //Method that handles actions of what happens when the mouse is pressed.
	void OnMouseDown()
	{
		//Get the position of the object being clicked.
		Vector3 click = gameObject.transform.position;
        //Using the position of the object in the world, get its position the screen.
		screenPoint = Camera.main.WorldToScreenPoint (click);
        //Base the mouse's movement in the z-direction off of its movement along the y-axis (up or down).
		mouseZ = Input.mousePosition.y;	
        //Make an array of all of the GameObjects in the scene.
		GameObject[] objects =  UnityEngine.Object.FindObjectsOfType<GameObject>();
        //Find out how big the array is -- how many objects there are.
		int size = objects.Length;
        //Set the size of the array that will hold the coordinates of the objects equal to the size
        //of the array that contains all of the GameObjects.
		coordinates = new Vector3[size];
        //Go through all of the objects in array.
		for (int item = 0; item < size; item ++)
		{
            //Determine what the selected object's index in the array is.
			if (click == objects[item].transform.position)
			{
				indexNumMatch = item;
			}
            //Add each object's position into the coordinates array.
			coordinates[item] = objects [item].transform.position;
		}


	}
	
    //This method determines what happens when the mouse is being dragged.
	void OnMouseDrag()
	{
        //Base the transformation of the object in the z-direction off of the change in y-position
        //from where the mouse was first clicked -- value saved as mouseZ.
		transformZ =(float) (.1*(Input.mousePosition.y - mouseZ));
        //If the "I" key has been pressed, make the transformation go in the z-direction.
		if (isPressed) 
		{
			curScreenPoint = new Vector3 (screenPoint.x, screenPoint.y,
			                              screenPoint.z + transformZ );
		}
        //If z-transformations have been turned off the transformation will simply be along the x- and y-directions.
		else
			curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //Since mouse input is being used, the positions must be converted from points on the screen to
        //points located in the world.
		curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
		//A boolean to make sure the object's initial position doesn't count towards collisions.
        bool matches = false;
        //Go through the array of coordinates so that the object isn't transformed on top of another object.
		for (int i = 0; i < coordinates.Length; i++) 
		{
			if(i != indexNumMatch && Mathf.Abs(coordinates[i].x - curPosition.x) <= 1 &&
			    Mathf.Abs(coordinates[i].y - curPosition.y) <= 1 &&
			    Mathf.Abs(coordinates[i].z - curPosition.z) <= 1)
				matches = true;
		}
        //As long as the object doesn't go through the "floor" and doesn't match 
        //the position of any other object, transform the position of the object.
		if (curPosition.y >= 0 && matches == false)
			transform.position = curPosition;
	}
}
