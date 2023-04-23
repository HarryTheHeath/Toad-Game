using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rainbow : MonoBehaviour
{
    public float _speed = 60f;
    
    private TextMeshProUGUI textMesh;
    private float hue;

    void Start()
    {
        // Get a reference to the TextMeshPro Text UI component
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Increment the hue value each frame to animate the color change
        hue += Time.deltaTime * _speed;

        // Clamp the hue value to keep it between 0 and 360
        if (hue >= 360f)
        {
            hue -= 360f;
        }

        // Set the color of the TextMeshPro Text UI component to the current hue value
        textMesh.color = Color.HSVToRGB(hue / 360f, 1f, 1f);
    }
}