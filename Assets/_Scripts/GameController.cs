using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject [] powerUps;
	public GameObject level;
	public HighScoreController highScores;
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
	public Text highScoreText;
	public InputField nameInput;
	public Button submitButton;
    public Canvas PauseMenu;

    private GameObject player;
	private int score;
	private int currentLevel;
	private int enemyCounter;
	private int maxEnemies;
	private int scoreValue;
    private int baseEnemies = 20;
    private int baseScore;
    private int difficulty;
	private bool restart;
	private bool gameOver;
	private bool bonus;
	private bool inBonus;
	private bool pause;
	private bool readyForNextLevel;
    private bool god;
    private bool inBoss;

	public Slider progressBar;
    
	void Start()
	{
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("Difficulty");
        }else{
            difficulty = 1;
        }
        switch (difficulty)
        {
            case 0:
                SetEasy();
                break;
            case 2:
                SetHard();
                break;
            case 1:
            default:
                SetNormal();
                break;
        }
        player = GameObject.FindWithTag("Player");
		restart = false;
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		highScoreText.text = "";
		currentLevel = 0;
		readyForNextLevel = true;
		score = 0;
		bonus = false;
        inBonus = false;
        inBoss = false;
		pause = false;
        god = false;
		UpdateScore ();
		nameInput.gameObject.SetActive (false);
		submitButton.gameObject.SetActive (false);
        //PlayerPrefs.DeleteAll ();  //Used to clear high score list
        PauseMenu.GetComponent<Canvas>();
        PauseMenu.enabled = false;
        progressBar.gameObject.SetActive(false);
    }

	public void setLevelText(string text){
		levelText.text = text;
	}

	void Update()
	{

		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				pause = false;
				Pause ();
				Application.LoadLevel (Application.loadedLevel);
			}
		}
				
		if(readyForNextLevel){
			toggleReadyForLevel();
            if(!bonus)
			    currentLevel+=1;
            SetMaxEnemies();
            UpdateScoreValue();
			spawnLevel();
		}
			
		if(Input.GetKeyDown (KeyCode.Escape)){
			pause = !pause;
            Pause();
		}
        if(Input.GetKeyDown(KeyCode.F1))
        {
			god = !god;
            ToggleGodMode();
        }

		if (gameOver){
            pause = true;
			restartText.text = "Press 'R' for Restart";
			restart = true;
		}
	}

    public void SetEasy()
    {
        spawnWait = 1.0f;
        startWait = 2f;
        waveWait = 4;
        baseEnemies = 15;
        baseScore = 5;
    }

    public void SetNormal()
    {
        spawnWait = 0.5f;
        startWait = 2f;
        waveWait = 3;
        baseEnemies = 20;
        baseScore = 10;
    }

    public void SetHard()
    {
        spawnWait = 0.20f;
        startWait = 2f;
        waveWait = 1.5f;
        baseEnemies = 30;
        baseScore = 20;
    }
    public void ToggleInBoss()
    {
        inBoss = !inBoss;
    }

    public bool GetInBoss()
    {
        return inBoss;
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

	public bool getBonus(){
		return bonus;
	}

	public void setBonus(bool temp){
		bonus = temp;
	}

	public bool getInBonus(){
		return inBonus;
	}

	public void setInBonus(bool temp){
		inBonus = temp;
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
		string gameOverMessage = "Game Over";
		gameOver = true;
		pause = true;
		Pause();
		highScores.LoadScores ();
		if (highScores.isHighScore (score)) {
			gameOverMessage += "\nNew High Score!";
			highScores.DisplayScoreInput ();
		} else {
			highScores.PrintScores ();
		}
		gameOverText.text = gameOverMessage;
	}

	public void SpawnPowerUp(Vector3 spawnPosition)
	{
		int powerUpChance = Random.Range(1,100);
		if (inBonus || inBoss) {
			GameObject powerUp = powerUps [Random.Range (0, powerUps.Length-1)];
			if (powerUpChance <= 25) {
				Instantiate (powerUp, spawnPosition, Quaternion.identity);
			}
		} else {
			GameObject powerUp = powerUps [Random.Range (0, powerUps.Length)];
			if (powerUpChance <= 15) {
				Instantiate (powerUp, spawnPosition, Quaternion.identity);
			}
		}
	}

    public int GetBaseEnemies()
    {
        return baseEnemies;
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
        maxEnemies = currentLevel*(int)Mathf.Log(currentLevel) + baseEnemies;
    }
	public void setMaxEnemies(int temp){
		maxEnemies = temp;
	}
	public int GetMaxEnemies(){
		return maxEnemies;
	}

	public void UpdateScoreValue(){
		scoreValue = currentLevel*(int)Mathf.Log(currentLevel)+baseScore;
	}

	public int GetScoreValue(){
		return scoreValue;
	}

    public void Pause()
    {
        if (pause)
        {
            if (gameOver == false)
                PauseMenu.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            
            PauseMenu.enabled = false;
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
