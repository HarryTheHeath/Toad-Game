
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class NewWaveSpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public Transform[] SpawnPoints;
    public float TimeBetweenSpawns;
    private float NextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > NextSpawnTime)
        {
            Instantiate(enemyTypes[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position,
                quaternion.identity);

            NextSpawnTime = Time.time + TimeBetweenSpawns;
        }
        
    }
}
