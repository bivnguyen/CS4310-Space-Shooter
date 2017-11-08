using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveManuver : MonoBehaviour {

	private Rigidbody rb;
	public Transform target;			//Player ship
	public float diveSpeed;				//Movement speed
	private Vector3	diveTarget;			//Static player location stored
	private Vector3 diveDestination;	//Dive return location opposite spawn location
	private bool isDiving;				//Check for when to move to dive return location

	void Start () {
		rb = GetComponent<Rigidbody> ();
		//Enemy ship will dive to players coordinates when enemy is spawned
		target = GameObject.FindWithTag("Player").transform;
		diveTarget = target.position;
		//Enemy ship will return to the opposite side of the game board it spawned on after diving
		diveDestination = new Vector3 (-rb.transform.position.x, rb.transform.position.y, rb.transform.position.z);
		isDiving = true;

	}

	void FixedUpdate () {
		//Check to sdee if dive has reached the diving target and flag to return to top of game board
		if (rb.transform.position == diveTarget) {
			isDiving = false;
		}
		//Once enemy has finished its dive and return animation, begin the dive and return animation back to original spawn
		if (rb.transform.position == diveDestination) {
			diveTarget = target.position;
			diveDestination = new Vector3 (-rb.transform.position.x, rb.transform.position.y, rb.transform.position.z);
			isDiving = true;
		}
		//Aim towards the dive target and move while diving
		if (target && isDiving) {
			rb.transform.LookAt (diveTarget);
			transform.Rotate (new Vector3 (0, 180, 0), Space.Self);
			transform.position = Vector3.MoveTowards (rb.transform.position, diveTarget, diveSpeed);
		} else {  //Aim towards the dive target and move while returning
			rb.transform.LookAt (diveTarget);
			transform.Rotate (new Vector3 (0, 180, 0), Space.Self);
			transform.position = Vector3.MoveTowards (rb.transform.position, diveDestination, diveSpeed);
		}

	}
}
