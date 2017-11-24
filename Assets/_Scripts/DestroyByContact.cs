using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start (){
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
		if(other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Boss" || other.tag == "Enemy Weapon")
		{
			return;
		}

		if(explosion != null){
			Instantiate(explosion, transform.position, transform.rotation);
		}
		if(other.tag == "Player"){
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		if(tag == "Enemy"){
			gameController.SpawnPowerUp(transform.position);
            gameController.DecrementEnemyCounter();
        }
       
		gameController.AddScore(scoreValue);
		if(other.tag != "God")     //god mode is for testing
			Destroy(other.gameObject);
		if(tag != "Boss"){
            Destroy(gameObject);

		}
	}
}
