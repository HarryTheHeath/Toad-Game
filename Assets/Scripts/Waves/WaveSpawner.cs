using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] Waves;
 
    private Wave _currentWave;
 
    public Transform[] _spawnPoints;
 
    private float _waveBuffer;
    private int i = 0;
 
    private bool _stopSpawning = false;
 
    private void Awake()
    {
 
        _currentWave = Waves[i];
        _waveBuffer = _currentWave.SpawnBuffer[0];
    }
 
    private void Update()
    { if
            (_stopSpawning)
            return;
        
        if (Time.time >= _waveBuffer)
        {
            SpawnWave();
            IncrementWave();
 
            _waveBuffer = Time.time + _currentWave.SpawnBuffer[0];
        }
    }
 
    private void SpawnWave()
    {
        //for (int i = 0; i < _currentWave.NumberToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, _currentWave.Enemy.Length);
            int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);
 
            Instantiate(_currentWave.Enemy[randomEnemy], _spawnPoints[randomSpawnPoint].position, _spawnPoints[randomSpawnPoint].rotation);
        }
    }
 
    private void IncrementWave()
    {
        
        // more waves to spawn
        if (i + 1 < Waves.Length)
        {
            i++;
            _currentWave = Waves[i];
        }
        
        else
            _stopSpawning = true;
    }
}