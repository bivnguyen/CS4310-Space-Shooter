using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOnContact : MonoBehaviour {

	private GameController gameController;
	private PlayerController gunObject;

	void Start()
	{
		
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}


	}
		
	void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
		{
			return;
		}
			
		Destroy (gameObject);

		//the code below does not seem to work
		//gunObject.gunSwitch();

	}

}