using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public float floatHeight = 0.1f;
    public float floatSpeed = 1.0f;
    
    private Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
        transform.position = initialPosition;
    }
    void Update()
    {
        float y = initialPosition.y + floatHeight * Mathf.Sin(Time.time * floatSpeed);
        transform.position = new Vector3(initialPosition.x, y, initialPosition.z);
    }
}