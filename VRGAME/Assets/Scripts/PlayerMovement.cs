using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float visibility; 
	public float audioV; 
	public bool prone = false; 
	public bool crouch = false; 

	public Transform targetMesh;

	private LineRenderer lineDrawer; 
	int segment = 50; 
	float xRadius = 8.0f;
	float yRadius = 8.0f;



	void Start ()
	{

		audioV = 8.0f; 
		speed = 4.5f;
		visibility = 1.0f; 
	}

	void audioLine()
	{
		lineDrawer = gameObject.GetComponent<LineRenderer> ();
		lineDrawer.positionCount = segment + 1; 
		lineDrawer.useWorldSpace = false; 
		lineDrawer.startWidth = 0.1f; 
		lineDrawer.endWidth = 0.1f; 

		xRadius = audioV;
		yRadius = audioV;

		float x; 
		float y; 

		float angle = 20.0f; 

		for (int i = 0; i < (segment + 1); i++) 
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * xRadius;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * yRadius;

			lineDrawer.SetPosition (i,new Vector3(x,-0.4f,y));
			angle += (360.0f / segment);
		}

	}

	void playAudio()
	{
		
	}

	void Update()
	{
		audioLine ();

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);


		if(Input.GetKeyUp(KeyCode.Space)&&((prone == true)||(crouch == true)))
		{
			prone = false; 
			crouch = false; 

			audioV = 8.0f; 
			speed = 4.5f; 
			visibility = 1.0f; 

			targetMesh.localScale = new Vector3(1,2,1);
		}
		if ((Input.GetKey(KeyCode.C))&&(crouch == false)) {
			crouch = true; 
			prone = false; 

			audioV = 6.0f; 
			speed = 2.5f; 
			visibility = 0.5f;

			targetMesh.localScale = new Vector3 (1, 1.2f, 1);
		} 
		if ((Input.GetKey(KeyCode.Z))&&(prone == false)) 
		{
			crouch = false; 
			prone = true; 

			audioV = 3.0f;
			speed = 1.0f;
			visibility = 0.3f; 

			targetMesh.localScale = new Vector3 (1, 0.7f, 1);
		}


		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			audioV = audioV * 2; 
			speed = speed * 2;
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			audioV = audioV / 2; 
			speed = speed / 2;
		}
			
	}




}

