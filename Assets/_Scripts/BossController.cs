using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
	public int hitPoints = 1;
	private GameController gameController;

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

	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}
		if(hitPoints > 0){
			hitPoints--;
		}

	}

	public int GetHitPoints(){
		return hitPoints;
	}
}
