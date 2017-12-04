using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    private GameController gameController;
    private int currentLevel;
    private int enemiesSpawned;
    private int maxEnemies;
    private int enemyCounter;
    public GameObject[] hazards;
    public GameObject[] bosses;

	private int progressBarMax;
	private int progressBarCurrent;


    void Start()
    {
        
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {   
            gameController = gameControllerObject.GetComponent<GameController>();
            currentLevel = gameController.GetCurrentLevel();
            Debug.Log("New level spawned. Level#" + currentLevel);
            maxEnemies = gameController.GetMaxEnemies();
            enemiesSpawned = 0;
            gameController.SetEnemyCounter(0);
            Debug.Log("Starting level. EnemyCounter = " + gameController.GetEnemyCounter() + " enemies spawned = " + enemiesSpawned + " maxEnemies = " + maxEnemies); 
			if (gameController.getBonus ()) {
				gameController.setLevelText("BONUS");
				StartCoroutine(spawnBonusLevel());
			}
			else 
            {
                gameController.setInBonus(false);
                if (currentLevel % 5 == 0) {
					Debug.Log (currentLevel);
                    gameController.ToggleInBoss();
					SpawnBoss ();
				} 
				else {
					Debug.Log (currentLevel);
					StartCoroutine(SpawnWaves());
				}
            }
        }
        if (gameController == null)
        {
            Debug.Log("cannot find 'GameController' script");
        }
    }


    void Update()
    {
        if (gameController.getBonus())
        {
            enemiesSpawned = maxEnemies;
        }
        if (gameController.GetInBoss())
        {
            gameController.SetEnemyCounter(1);
        }
		if (gameController.GetEnemyCounter () <= 0 && enemiesSpawned >= maxEnemies && !gameController.GetInBoss()) {
            Debug.Log("Destroying level controller. EnemyCounter = " + gameController.GetEnemyCounter() + " enemies spawned = " + enemiesSpawned + " maxEnemies = " + maxEnemies);
            gameController.SetEnemyCounter(0);
			gameController.toggleReadyForLevel ();
			Destroy (gameObject);
		//} else if (gameController.getBonus ()) {
		//	gameController.toggleReadyForLevel ();
		//	Destroy (gameObject);
		}
    }

    void SpawnBoss()
    {
        maxEnemies = 1;
        GameObject boss = bosses[Random.Range(0, bosses.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-gameController.spawnValues.x, gameController.spawnValues.x), gameController.spawnValues.y, gameController.spawnValues.z-2);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
        gameController.IncrementEnemyCounter();
        enemiesSpawned = 1;
    }

    IEnumerator SpawnWaves()
    {
        for (enemiesSpawned = 0; enemiesSpawned <= maxEnemies;)
        {
            yield return new WaitForSeconds(gameController.startWait);
            for (int i = 0; i < gameController.hazardCount; i++, enemiesSpawned++)
            {
                if (enemiesSpawned >= maxEnemies)
                {
                    break;
                }
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-gameController.spawnValues.x, gameController.spawnValues.x), gameController.spawnValues.y, gameController.spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                gameController.IncrementEnemyCounter();
                Debug.Log("Enemy spawned: Enemy Counter = " + gameController.GetEnemyCounter());
                yield return new WaitForSeconds(gameController.spawnWait);
            }
            yield return new WaitForSeconds(gameController.waveWait);
        }
    }

	IEnumerator spawnBonusLevel(){
		Debug.Log ("Spawning bonus level. currentLevel = " + currentLevel);
		gameController.setInBonus (true);
		gameController.setBonus (false);
		gameController.progressBar.gameObject.SetActive (true);
		int levelsTilBoss = levelsTillBoss();

		gameController.SetCurrentLevel (currentLevel + levelsTilBoss - 1);
		gameController.UpdateScoreValue();

		maxEnemies = (currentLevel*(int)Mathf.Log(currentLevel) + gameController.GetBaseEnemies()) * (levelsTilBoss);   //use maxEnemies since this is in the destruction criteria
		Debug.Log (maxEnemies);

		gameController.progressBar.maxValue = maxEnemies;
		//progressBar.maxValue = maxEnemies;

		yield return new WaitForSeconds(gameController.startWait);

		for (enemiesSpawned = 0; enemiesSpawned <= maxEnemies; enemiesSpawned++) 
		{  //need to increment enemiesSpawned since it's in the destructionc criteria
			GameObject hazard = hazards [Random.Range (0, 3)];
			Vector3 spawnPosition = new Vector3 (Random.Range (-gameController.spawnValues.x, gameController.spawnValues.x), gameController.spawnValues.y, gameController.spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (hazard, spawnPosition, spawnRotation);
			gameController.progressBar.value++; 
			gameController.IncrementEnemyCounter();				//increment enemy counter otherwise update will destroy the level as soon as you leave the for loop
            Debug.Log("Enemy spawned: Enemy Counter = " + gameController.GetEnemyCounter());
            yield return new WaitForSeconds (gameController.spawnWait);
		}

		if (enemyCounter == 0) 
		{
			resetProgressBar ();
		}
		//This is handled in update
		//gameController.toggleReadyForLevel ();  
		//Destroy (gameObject);
		Debug.Log ("Ending Bonus level");
	}

	public int levelsTillBoss () {
		int lastDigit = currentLevel % 5;

		if (lastDigit == 1) 
		{
			return 4;
		} 
		else if (lastDigit == 2) 
		{
			return 3;
		} 
		else if (lastDigit == 3)
		{
			return 2;
		}
        else if (lastDigit == 4)
        {
            return 1;
        }

		return 0;
	}

	int getProgressBarMax()
	{
		return progressBarMax;
	}

	void setProgressBarMax(int max)
	{
		progressBarMax = max;
	}

	int getProgressBarCurrent()
	{
		return progressBarCurrent;
	}

	void setProgressBarCurrent(int addToProgressBar)
	{
		progressBarCurrent += addToProgressBar;
	}

	void resetProgressBar()
	{
		gameController.progressBar.value = 0;
		gameController.progressBar.gameObject.SetActive (false);
	}		
}
