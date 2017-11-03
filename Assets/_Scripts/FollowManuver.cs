using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManuver : MonoBehaviour {

	private Rigidbody rb;
	public Transform target;
	public float chaseSpeed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (target) {
			rb.transform.LookAt (target.position);
			transform.Rotate (new Vector3 (0, 180, 0), Space.Self);
			transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed);
		}
	}
}
