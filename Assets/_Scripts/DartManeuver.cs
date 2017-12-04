using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartManeuver : MonoBehaviour {

	private Rigidbody rb;
	private Vector3	diveTarget;			//Static player location stored
	private bool rotated;
	private GameObject playerShip;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rotated = false;	//Mark that ship needs to be rotated to face player
	}

	void FixedUpdate () {
		
		//Check that player is alive, and that ship is not aimed at target
		if ((GameObject.FindWithTag("Player") || GameObject.FindWithTag("God")) && !rotated) {
			playerShip = GameObject.FindWithTag ("Player");

			if (playerShip == null) {
				playerShip = GameObject.FindWithTag ("God");
			}
			diveTarget = playerShip.transform.position;

			rb.transform.LookAt (diveTarget);									//Aim towards player location
			transform.Rotate (new Vector3 (0, 180, 0), Space.Self);				//Rotate to face player
			rotated = true;														//Mark that ship is now facing player
		} else {
			rb.velocity = transform.forward * -5;	//Move towards player
		}
	}
	//On exiting the dive boundary respawn the ship in the game spawn zone
	void OnTriggerExit(Collider other) {
		if (other.tag == "MoveBound") {
			rotated = false;
			rb.transform.position = new Vector3 (Random.Range (-5.9f, 5.9f), 0, 17.4f);
		}
	}
}
