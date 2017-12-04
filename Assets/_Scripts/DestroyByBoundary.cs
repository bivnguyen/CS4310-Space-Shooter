using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
    private GameController gameController;

    void Start()
    {
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
    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        if (other.tag == "Enemy")
        {
            gameController.DecrementEnemyCounter();
            Debug.Log("Destroy by boundary: Enemy Counter = " + gameController.GetEnemyCounter());
        }
        Destroy(other.gameObject);
    }
}
