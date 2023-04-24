using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelfDestructableTutorialElement : MonoBehaviour
{

    public enum KEY
    {
        A,
        D,
        space,
        mouse
    }

    public KEY input;

    private void Start()
    {
        if(PlayerPrefs.GetInt(input.ToString()) == 1) Destroy(gameObject);
    }

    void Update()
    {
        switch (input)
        {
            case KEY.A:
                if(Input.GetKeyDown(KeyCode.A)) Destroy(gameObject);
                break;
            case KEY.D:
                if(Input.GetKeyDown(KeyCode.D)) Destroy(gameObject);
                break;
            case KEY.space:
                if(Input.GetKeyDown(KeyCode.Space)) Destroy(gameObject);
                break;
            case KEY.mouse:
                if(Input.GetMouseButtonDown(0)) Destroy(gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(input.ToString(), 1);
    }
}