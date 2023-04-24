using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider Fill;

    public void SetMaxHealth(int i, bool fillHealth = true)
    {
        Fill.maxValue = i;
        if (fillHealth) Fill.value = i;
    }
    
    public void SetHealth(int i)
    {
        Fill.value = i;
    }
}
