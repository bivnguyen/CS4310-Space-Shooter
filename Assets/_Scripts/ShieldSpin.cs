using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpin : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Transform target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 360.0f;
    private Vector3 relativeDistance = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            player = GameObject.FindWithTag("God");
        }
        target = player.GetComponent<Transform>();
        if (target != null)
        {
            relativeDistance = transform.position - target.position;
        }
    }

    void LateUpdate()
    {

        Orbit();

    }

    void Orbit()
    {
        if (target != null)
        {
            // Keep us at the last known relative position
            transform.position = target.position + relativeDistance;
            transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
            // Reset relative position after rotate
            relativeDistance = transform.position - target.position;
        }
    }
}







