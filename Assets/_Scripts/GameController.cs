using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject[] hazards;
	public GameObject [] powerUps;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private bool restart;
	private bool gameOver;

	void Start()
	{	
		restart = false;
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{	
		while(true){
			yield return new WaitForSeconds (startWait);
			for(int i = 0;i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0,hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x,spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}



	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void SpawnPowerUp(Vector3 spawnPosition)
	{
		int powerUpChance = Random.Range(1,100);
		GameObject powerUp = powerUps[Random.Range(0,powerUps.Length)];
		if(powerUpChance <=5){
			Instantiate (powerUp, spawnPosition, Quaternion.identity);
		}
	}
	//Functions to add
	//spawnPowerUps
	//changeLevel
	//pause
	//menu
	//
	
}
