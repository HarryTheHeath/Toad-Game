using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class NewWaveSpawner : MonoBehaviour
{
 
    public Wave[] Wave;
    public float SpawnTimeMultiplier = 0.5f;
    public float WaveBuffer = 3f;
    private float NextSpawnTime;
    private int CurrentWave;

    // Start is called before the first frame update
    private void Awake() => NextSpawnTime += WaveBuffer;

    private void Start()
    {
        StartCoroutine(WaveWait(WaveBuffer));
        StartCoroutine(CommenceWaves());
        
    }
    

    private IEnumerator WaveWait(float WaveTime)
    {
        yield return new WaitForSeconds(WaveTime);
    }
    
    

    private IEnumerator CommenceWaves()
    {
        for (var i = 0; i < Wave[CurrentWave].Enemy.Length; i++)
        {
            Instantiate(Wave[CurrentWave].Enemy[i], Wave[CurrentWave].SpawnPoint[i].position,
                quaternion.identity);
                
            NextSpawnTime = Wave[CurrentWave].SpawnBuffer[i] + Random.Range(-SpawnTimeMultiplier, SpawnTimeMultiplier);
            Debug.Log($"NextEnemyComing in... {NextSpawnTime} seconds");

            yield return new WaitForSeconds(NextSpawnTime);
        }
        
        Debug.Log("Wave Over");
        CurrentWave++;
    }
    
    
}
