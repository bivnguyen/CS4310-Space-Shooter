using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject [] powerUps;
	public GameObject level;
	public GameObject HighScores;
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

    private GameObject player;
	private int score;
	private int currentLevel;
	private int enemyCounter;
	private int maxEnemies;
	private int scoreValue;
	private bool restart;
	private bool gameOver;
	private bool bonus;
	private bool pause;
	private bool readyForNextLevel;
    private bool god;
    
	void Start()
	{
        player = GameObject.FindWithTag("Player");
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
        god = false;
		UpdateScore ();

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
		//if(!gameOver){
			//if(bonus){
			//	//spawnBonusLevel();
			//}
			//else{
				
				if(readyForNextLevel){
					toggleReadyForLevel();
					currentLevel+=1;
                    SetMaxEnemies();
                    UpdateScoreValue();
					spawnLevel();
				}
			//}
			if(Input.GetKeyDown (KeyCode.Escape)){
				pause = !pause;
                Pause();
			}
            if(Input.GetKeyDown(KeyCode.F1))
            {
                god = !god;
                ToggleGodMode();
            }
		//}
		if (gameOver){
            pause = true;
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

	public void AddScore(){
		score += scoreValue;
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
        levelText.text = "Level " + currentLevel;
        Instantiate(level, transform.position, Quaternion.identity);
	}

	public void DecrementEnemyCounter(){
		enemyCounter--;
	}

    public void IncrementEnemyCounter()
    {
        enemyCounter++;
    }

	public int GetEnemyCounter(){
		return enemyCounter;
	}

    public void SetEnemyCounter(int num)
    {
        enemyCounter = num;
    }
	public void SetCurrentLevel(int newLevel){
		currentLevel = newLevel;
	}

    public void SetMaxEnemies()
    {
        maxEnemies = currentLevel*(int)Mathf.Log(currentLevel) + 20;
    }
	public int GetMaxEnemies(){
		return maxEnemies;
	}

	private void UpdateScoreValue(){
		scoreValue = currentLevel*(int)Mathf.Log(currentLevel)+10;
	}

	public int GetScoreValue(){
		return scoreValue;
	}

    public void Pause()
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void ToggleGodMode()
    {
        if (god)
        {
            player.tag = "God";
        }
        else
        {
            player.tag = "Player";
        }
    }
}
