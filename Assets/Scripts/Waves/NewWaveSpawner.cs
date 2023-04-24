using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class NewWaveSpawner : MonoBehaviour
{
 
    public Wave[] Wave;
    public float SpawnTimeMultiplier = 0.5f;
    public float WaveBuffer = 3f;
    [HideInInspector]
    public int CurrentWave;
    private float NextSpawnTime;

    // Start is called before the first frame update
    private void Awake() => NextSpawnTime += WaveBuffer;

    private void Start()
    {
        StartCoroutine(WaveWait(WaveBuffer));
        StartCoroutine(LoopOfCoroutines(CurrentWave));
    }
    
    
    private IEnumerator WaveWait(float WaveTime) { yield return new WaitForSeconds(WaveTime); }


    private IEnumerator LoopOfCoroutines(int currentWave)
    {
        
        for (int i = 0; i < Wave.Length; i++)
        {
            Debug.Log($"Wave {i} begins...");
            yield return StartCoroutine(CommenceWaves(currentWave));
        }
    }
    
    

    private IEnumerator CommenceWaves(int currentWave)
    {
        for (var i = 0; i < Wave[currentWave].Enemy.Length; i++)
        {
            Instantiate(Wave[currentWave].Enemy[i], Wave[currentWave].SpawnPoint[i].position,
                quaternion.identity);
                
            NextSpawnTime = Wave[currentWave].SpawnBuffer[i] + Random.Range(-SpawnTimeMultiplier, SpawnTimeMultiplier);
            Debug.Log($"NextEnemyComing in... {NextSpawnTime} seconds");

            yield return new WaitForSeconds(NextSpawnTime);
        }

        Debug.Log("Wave Over");
        yield return new WaitForSeconds(WaveBuffer);
        CurrentWave++;
    }
}
