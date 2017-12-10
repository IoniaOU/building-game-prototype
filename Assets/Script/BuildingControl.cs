using UnityEngine;
using System.Collections;

public class BuildingControl : MonoBehaviour
{
	public int buildingType;
	public bool onFocus;
	public Color originalColor;
	public Color focusColor;
	public Color unableColor;
	public bool buildPermission = true;
	void Start ()
	{
		originalColor = gameObject.GetComponent<Renderer>().material.color;
		focusColor = new Color(gameObject.GetComponent<Renderer>().material.color.r+0.2f,gameObject.GetComponent<Renderer>().material.color.g+0.2f,gameObject.GetComponent<Renderer>().material.color.b+0.2f);
		unableColor = Color.red;
	}

	void Update ()
	{
		if(onFocus)
		{
			gameObject.GetComponent<Renderer>().material.color = focusColor;
		}
		else if(!gameObject.GetComponent<BoxCollider>().isTrigger)
		{
			gameObject.GetComponent<Renderer>().material.color = originalColor;
		}
		onFocus = false;
	}

	void OnTriggerStay(Collider other)
	{
		gameObject.GetComponent<Renderer>().material.color = unableColor;
		buildPermission = false;
	}
	void OnTriggerExit(Collider other)
	{
		gameObject.GetComponent<Renderer>().material.color = originalColor;
		buildPermission = true;
	}
}
