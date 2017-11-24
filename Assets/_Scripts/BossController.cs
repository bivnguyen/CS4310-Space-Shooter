using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
	public int hitPoints = 1;
	private GameController gameController;
    private AudioSource audiosource;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    // Use this for initialization
    void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
			hitPoints = gameController.GetCurrentLevel();
		}
		if (gameController == null)
		{
			Debug.Log("cannot find 'GameController' script");
		}
        audiosource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);

    }

	void OnTriggerEnter(Collider other){
		if(other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Enemy Weapon")
		{
			return;
		}
		if(hitPoints > 0){
			hitPoints--;
		}

	}

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audiosource.Play();
    }

    public int GetHitPoints(){
		return hitPoints;
	}
}
