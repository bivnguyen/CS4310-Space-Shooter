/*


PROBABLY NOT GONNA USE THIS FILE


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOnContact : MonoBehaviour {

	private GameController gameController;
	private PlayerController gunObject = new PlayerController();


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
		
		 
	//the only thing below that works is the Destroy (gameobject);

	void OnTriggerEnter(Collider other)
	{
		/*
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
		{
			return;
		}
		*/

		if (other.tag == "Player" && tag == "MultiShot")
		{
			//gunObject.gunSwitch ();

			Destroy (gameObject);

		}

		else if (other.tag == "Player" && tag == "FireRate")
		{
			Destroy (gameObject);
			//gunObject.gunSwitch ();
		}
			

	}


}