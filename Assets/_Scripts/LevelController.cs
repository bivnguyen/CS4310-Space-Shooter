using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    private GameController gameController;
    private int currentLevel;
    private int enemiesSpawned;
    private int maxEnemies;
    private int enemyCounter;
    public GameObject[] hazards;
    public GameObject[] bosses;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            currentLevel = gameController.GetCurrentLevel();
            maxEnemies = gameController.GetMaxEnemies();
            enemiesSpawned = 0;
            gameController.SetEnemyCounter(0);
			 
			if (gameController.getBonus ()) {
				gameController.setLevelText("BONUS");
				StartCoroutine(spawnBonusLevel());
			}
			else 
            {
				if (currentLevel % 5 == 0) {
					Debug.Log (currentLevel);
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
		if (gameController.GetEnemyCounter () <= 0 && enemiesSpawned >= maxEnemies) {
			gameController.toggleReadyForLevel ();
			Destroy (gameObject);
		} else if (gameController.getBonus ()) {
			gameController.toggleReadyForLevel ();
			Destroy (gameObject);
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
        for (enemiesSpawned = 0; enemiesSpawned < maxEnemies;)
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
                yield return new WaitForSeconds(gameController.spawnWait);
            }
            yield return new WaitForSeconds(gameController.waveWait);
        }
    }

	IEnumerator spawnBonusLevel(){
		Debug.Log ("Spawning bonus level");
		gameController.setInBonus (true);
		gameController.setBonus (false);

		maxEnemies = (currentLevel*(int)Mathf.Log(currentLevel) + 20) * (levelsTillBoss());   //use maxEnemies since this is in the destruction criteria
		Debug.Log (maxEnemies);
		yield return new WaitForSeconds(gameController.startWait);

		for (enemiesSpawned = 0; enemiesSpawned < maxEnemies; enemiesSpawned++) {  //need to increment enemiesSpawned since it's in the destructionc criteria
			GameObject hazard = hazards [Random.Range (0, 3)];
			Vector3 spawnPosition = new Vector3 (Random.Range (-gameController.spawnValues.x, gameController.spawnValues.x), gameController.spawnValues.y, gameController.spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (hazard, spawnPosition, spawnRotation);
			gameController.IncrementEnemyCounter();				//increment enemy counter otherwise update will destroy the level as soon as you leave the for loop
			yield return new WaitForSeconds (gameController.spawnWait);
		}

		gameController.SetCurrentLevel (currentLevel += levelsTillBoss());

		//This is handled in update
		//gameController.toggleReadyForLevel ();  
		//Destroy (gameObject);
		gameController.setInBonus (false);
		Debug.Log ("Ending Bonus level");
	}

	public int levelsTillBoss () {
		int lastDigit = currentLevel % 10;

		if (lastDigit == 1 || lastDigit == 6) {
			return 3;
		} else if (lastDigit == 2 || lastDigit == 7) {
			return 2;
		} else if (lastDigit == 3 || lastDigit == 8) {
			return 1;
		}

		return 0;
	}
}
