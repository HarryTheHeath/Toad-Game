using UnityEngine;
 
[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField]
    public GameObject[] Enemy { get; private set; }
    
    [field: SerializeField]
    public float[] SpawnBuffer { get; private set; }
    
    
    [field: SerializeField]
    public Transform[] SpawnPoint { get; private set; }
}