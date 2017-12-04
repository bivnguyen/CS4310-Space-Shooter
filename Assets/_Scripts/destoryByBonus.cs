using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryByBonus : MonoBehaviour {
	private GameController gameController;
	public GameObject explosion;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
		{
			Debug.Log("cannot find 'GameController' script");
		}
	}
	

	void Update () {
		if (gameController.getBonus())
		{
			if(explosion != null)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}
			
            if(tag == "Enemy")
            {
                gameController.DecrementEnemyCounter();
            }
            Debug.Log("Destroyed Enemy by Bonus Enemy counter = " + gameController.GetEnemyCounter());
            Destroy(gameObject);
		}
	}
}
