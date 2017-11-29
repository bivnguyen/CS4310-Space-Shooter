using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	static private bool isShieldOn;


	void Start (){
		isShieldOn = false;

		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
		{
			Debug.Log("cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Enemy Weapon" || other.tag == "Boss" || 
			other.tag == "MultiShot" || other.tag == "FireRate" || other.tag == "SpeedBoost" || other.tag == "Shield" || 
			other.tag == "bonusPowerUp" || other.tag == "MoveBound")


		{
			return;
		}

		if(explosion != null){
			Instantiate(explosion, transform.position, transform.rotation);
		}
		if(other.tag == "Player" && !isShieldOn){
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		if(tag == "Enemy"){
			gameController.SpawnPowerUp(transform.position);
            gameController.DecrementEnemyCounter();
        }
       
		gameController.AddScore(scoreValue);
		//if(other.tag != "Player")     //god mode for testing
		if (!isShieldOn && other.tag != "God")
		{
			Destroy (other.gameObject);
		}

		if(tag != "Boss"){
            Destroy(gameObject);

		}
	}

	public void shieldSwitch(bool x)
	{
		isShieldOn = x;
	}

}


