using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBounceManeuver : MonoBehaviour {

	private const float SPEED = 5f;
	private Vector3 direction;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		direction = (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f))).normalized;
	}

	void Update()
	{
		rb.transform.position += direction * SPEED * Time.deltaTime;
	}

	void OnTriggerExit(Collider other)
	{
		float xmin, xmax, zmin, zmax;
		xmin = zmin = -1.0f;
		xmax = zmax = 1.0f;

		if (other.tag == "MoveBound") {
			if (rb.position.x >= 5) {
				xmax = -0.1f;
			}
			if (rb.position.x <= -5) {
				xmin = 0.1f;
			}
			if (rb.position.z >= 15) {
				zmax = -0.1f;
			}
			if (rb.position.z <= -1) {
				zmin = -0.1f;
			}

			direction = (new Vector3 (Random.Range (xmin, xmax), 0.0f, Random.Range (zmin, zmax))).normalized;
		}
	}
}