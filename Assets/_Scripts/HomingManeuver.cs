using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingManeuver : MonoBehaviour {

	private Rigidbody rb;
	private Transform target;		//Player ship
	public float pullSpeedFactor;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		target = GameObject.FindWithTag("Player").transform;
		if (target == null) {
			target = GameObject.FindWithTag("God").transform;
		}
	}

	void FixedUpdate () {
		//While player is alive, home towards the player with magnetic like force
		if (target) {
			Vector3 relativePos = target.position - rb.transform.position;
			rb.AddForce(relativePos*(pullSpeedFactor/(relativePos.magnitude*relativePos.magnitude)));
		}
	}
}

