using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinShot : MonoBehaviour {

	public float spinRate;
	private Rigidbody rb;
	private GameObject bossObject;
	private Transform bossTransform;
	// Use this for initialization
	void Start () {
		bossObject = GameObject.FindWithTag("Boss");
		bossTransform = bossObject.GetComponent<Transform>();

	
	}

	void FixedUpdate(){
		transform.RotateAround(bossTransform.position,bossTransform.up, 100*Time.deltaTime);
	}
}
