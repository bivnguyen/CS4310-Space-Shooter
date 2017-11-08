using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject [] powerUps;
	public GameObject level;
	public Vector3 spawnValues;
	public int hazardCount;
	public int waveCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text levelText;

	private int score;
	private int currentLevel;
	private bool restart;
	private bool gameOver;
	private bool bonus;
	private bool pause;
	private bool readyForNextLevel;

	void Start()
	{	
		restart = false;
		gameOver = false;
		bonus = false;
		restartText.text = "";
		gameOverText.text = "";
		currentLevel = 0;
		readyForNextLevel = true;
		score = 0;
		bonus = false;
		pause = false;
		UpdateScore ();

	}

	void Update()
	{
		if(pause){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1;
		}
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}

		}
		//if(!gameOver){
			//if(bonus){
			//	//spawnBonusLevel();
			//}
			//else{
				levelText.text = "Level " + currentLevel;
				if(readyForNextLevel){
					toggleReadyForLevel();
					currentLevel+=1;

					spawnLevel();
					//do{}while(!readyForNextLevel);

				}
			//}
			if(Input.GetKeyDown (KeyCode.Escape)){
				pause = !pause;
			}
		//}
		if (gameOver){
			restartText.text = "Press 'R' for Restart";
			restart = true;
		}
	}

	public void toggleReadyForLevel(){
		if(readyForNextLevel){
		readyForNextLevel = false;
		}
		else{
			readyForNextLevel = true;
		}

	}
	public bool GetGameOver(){
		return gameOver;
	}

	public int GetCurrentLevel(){
		return currentLevel;
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

	void spawnLevel (){
		Instantiate(level, transform.position, Quaternion.identity);
	}
	//Functions to add
	//pause
	//menu
	//
	
}
