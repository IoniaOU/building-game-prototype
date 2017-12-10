using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	private float verticalMove;
	private float horizontalMove;

	void Start ()
	{
	
	}

	void Update ()
	{
		verticalMove = Input.GetAxis("Vertical");
		horizontalMove = Input.GetAxis("Horizontal");
	}
	void FixedUpdate()
	{
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Camera.main.transform.position.x+horizontalMove,Camera.main.transform.position.y,Camera.main.transform.position.z+verticalMove),1.0f);
	}
}
