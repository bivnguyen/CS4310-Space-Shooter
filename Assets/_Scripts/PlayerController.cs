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

	private GameController gameController;

	// the stuff below is for multi shot power up
	private bool multiShot;
	public Transform shotSpawn1; 
	public Transform shotSpawn2; 
	public Transform shotSpawn3; 
	private int multiShotAmmo;

	//gunSwitch doesn't seem to work when I use it from PickupOnContact.cs
	public void gunSwitch()
	{
		multiShot = true;
	}

	void Start ()
	{
		multiShot = false;
		multiShotAmmo = 0;
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{


		if(Input.GetButton ("Fire1") && Time.time > nextFire){

			if (multiShotAmmo > 0)
			{
				FireShots ();
				multiShotAmmo--;
				//StartCoroutine ("PowerupTimer", 0); // will last 10 seconds. see poweruptimer code below
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
			// turns on multishot power up
			//multiShot = true;

			//having a fixed ammo amount might be better than being timed
			//easily stackable
			multiShotAmmo += 20;
			Destroy (other.gameObject);
		}

		else if (other.tag == "FireRate")
		{
			// wait time for firing guns multiplied by multiple of .7f seconds.
			fireRate *= 0.7f;
			Destroy (other.gameObject);
			StartCoroutine ("PowerupTimer", 0);

		}
		/* // the following does not exist at the moment
		else if (other.tag == "SpeedBoost")
		{
			
			Destroy (other.gameObject);
		}
		*/

	}
		

	void FireShots()
	{
		// the second and third bullet for the multiShot power up
		GameObject Bullet2 = (GameObject)Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);
		Bullet2.transform.position = shotSpawn2.transform.position;

		GameObject Bullet3 = (GameObject)Instantiate (shot, shotSpawn3.position, shotSpawn3.rotation);
		Bullet3.transform.position = shotSpawn3.transform.position;
	}


	IEnumerator PowerupTimer()
	{
		// whatever power up this was used for will last 15f seconds
		// at the moment, obtaining multiple power ups with these will cause problems
		yield return new WaitForSeconds(15f);
		//multiShot = false;
		fireRate /= 0.7f;
	}



}
