
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class NewWaveSpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public Transform[] SpawnPoints;
    public float TimeBetweenSpawns;
    public float WaveBuffer = 3f;
    private float NextSpawnTime;

    // Start is called before the first frame update
    void Start() => NextSpawnTime += WaveBuffer;

    // Update is called once per frame
    void Update()
    {
        if (Time.time < WaveBuffer)
            return;
        
        if (Time.time > NextSpawnTime)
        {
            Instantiate(enemyTypes[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position,
                quaternion.identity);

            NextSpawnTime = Time.time + TimeBetweenSpawns;
        }
        
    }
}
