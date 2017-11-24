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
    //	public Vector3 spawnValues;
    //	public int hazardCount;
    //	public int waveCount;
    //	public float spawnWait;
    //	public float startWait;
    //	public float waveWait;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            //gameController.toggleReadyForLevel();
            currentLevel = gameController.GetCurrentLevel();
            maxEnemies = gameController.GetMaxEnemies();
            enemiesSpawned = 0;
            gameController.SetEnemyCounter(0);
            if (currentLevel % 1 == 0)
            {
                SpawnBoss();
            }
            else
            {
                StartCoroutine(SpawnWaves());
            }
        }
        if (gameController == null)
        {
            Debug.Log("cannot find 'GameController' script");
        }

    }


    void Update()
    {
        if (gameController.GetEnemyCounter() <= 0 && enemiesSpawned >= maxEnemies)
        {
            gameController.toggleReadyForLevel();
            Destroy(gameObject);
        }
    }

    void SpawnBoss()
    {
        maxEnemies = 1;
        GameObject boss = bosses[Random.Range(0, bosses.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-gameController.spawnValues.x, gameController.spawnValues.x), gameController.spawnValues.y, gameController.spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
        gameController.IncrementEnemyCounter();
        enemiesSpawned = 1;
    }

    IEnumerator SpawnWaves()
    {
        for (enemiesSpawned = 0; enemiesSpawned < maxEnemies;)
        {
            //System.Threading.Thread.Sleep((int)startWait*1000);
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
                //System.Threading.Thread.Sleep((int)spawnWait*1000);
                yield return new WaitForSeconds(gameController.spawnWait);
            }
            yield return new WaitForSeconds(gameController.waveWait);
            //System.Threading.Thread.Sleep((int)waveWait*1000);

        }
    }
}
