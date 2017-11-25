using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb;
	public float tilt;
	public float speed;
	public Boundary boundary;
	public GameObject shot;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	private AudioSource audioSource;

	// the stuff below is for multi shot power up
	public bool multiShot;
	public Transform shotSpawn1; 
	public Transform shotSpawn2; 
	public Transform shotSpawn3; 

	//gunSwitch doesn't seem to work
	public void gunSwitch()
	{
		multiShot = true;
	}

	void Start ()
	{
		//multiShot = false;
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if(Input.GetButton ("Fire1") && Time.time > nextFire){

			if (multiShot)
			{
				FireShots ();
			}

			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn1.position, shotSpawn1.rotation);// as GameObject;
			audioSource.Play();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "MultiShot")
		{
			multiShot = true;
		}
	}



	void FireShots()
	{
		GameObject Bullet2 = (GameObject)Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);
		Bullet2.transform.position = shotSpawn2.transform.position;

		GameObject Bullet3 = (GameObject)Instantiate (shot, shotSpawn3.position, shotSpawn3.rotation);
		Bullet3.transform.position = shotSpawn3.transform.position;
	}

}
