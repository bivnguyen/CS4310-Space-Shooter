using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByHealth : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	private BossController bossController;

	void Start (){
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		GameObject bossObject = GameObject.FindWithTag("Boss");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
			scoreValue = gameController.GetCurrentLevel()*10;
		}
		if (gameController == null)
		{
			Debug.Log("cannot find 'GameController' script");
		}
		if(bossObject != null){
			bossController = bossObject.GetComponent<BossController>();
		}
		if(bossController == null){
			Debug.Log("Cannot find 'BossController' script");
		}

	}

	
	// Update is called once per frame
	void Update () {
		if(bossController.GetHitPoints() <= 0){
			if(explosion != null){
				Instantiate(explosion, transform.position, transform.rotation);
			}
			gameController.AddScore(scoreValue);
			Destroy(gameObject);
		}
	}
}
