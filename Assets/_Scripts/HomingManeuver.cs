using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingManeuver : MonoBehaviour {

	private Rigidbody rb;
	private Transform target;		//Player ship
	private GameObject playerShip;
	public float pullSpeedFactor;

	void Start () {
		rb = GetComponent<Rigidbody> ();

		playerShip = GameObject.FindWithTag ("Player");

		if (playerShip == null) {
			playerShip = GameObject.FindWithTag ("God");
		}
		target = playerShip.transform;
	}

	void FixedUpdate () {
		//While player is alive, home towards the player with magnetic like force
		if (target) {
			Vector3 relativePos = target.position - rb.transform.position;
			rb.AddForce(relativePos*(pullSpeedFactor/(relativePos.magnitude*relativePos.magnitude)));
		}
	}
}

