using UnityEngine;
using System.Collections;

public class PlayerSpawnController : MonoBehaviour {

    public GameObject player;
    public Transform[] spawnPoints;
    public int startSpawn;

	void Start ()
    {
        Spawn();
	}

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (startSpawn < spawnPoints.Length - 1)
            {
                startSpawn++;
            }
            else
            {
                startSpawn = 0;
            }
            Spawn();
        }
    }

    void Spawn()
    {
        player.transform.position = spawnPoints[startSpawn].position;
        player.transform.rotation = spawnPoints[startSpawn].rotation;
    }
}
