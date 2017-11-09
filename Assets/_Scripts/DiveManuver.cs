using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveManuver : MonoBehaviour {

	private Rigidbody rb;
	public Transform target;			//Player ship
	private Vector3	diveTarget;			//Static player location stored
	private bool rotated;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		//Enemy ship will dive to players coordinates when enemy is spawned
		target = GameObject.FindWithTag("Player").transform;
		diveTarget = target.position;
		rotated = false;
		//Enemy ship will return to the opposite side of the game board it spawned on after diving

	}

	void FixedUpdate () {
		if (target && !rotated) {
			target = GameObject.FindWithTag("Player").transform;
			diveTarget = target.position;
			rb.transform.LookAt (diveTarget);
			transform.Rotate (new Vector3 (0, 180, 0), Space.Self);
			rotated = true;
		} else {
			rb.velocity = transform.forward * -5;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "DiveBoundary") {
			rotated = false;
			rb.transform.position = new Vector3 (Random.Range (-5.9f, 5.9f), 0, 17.4f);
		}
	}
}
