using UnityEngine;
 
[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField]
    public GameObject[] EnemiesInWave { get; private set; }
    
    [field: SerializeField]
    public float TimeBeforeThisWave { get; private set; }
 
    [field: SerializeField]
    public float NumberToSpawn { get; private set; }
}