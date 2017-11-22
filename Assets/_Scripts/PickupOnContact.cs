using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOnContact : MonoBehaviour {

	private GameController gameController;

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

		// some code here to makes it so that you have the power up
		// i have no idea if this code even works though

		Destroy (gameObject);

	}

}