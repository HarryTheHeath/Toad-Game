using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Serialization;

public class Snake : MonoBehaviour
{
    public float _speed = 4;
    public AudioClip _enter;
    public AudioClip _die;
    public AudioClip _eat;
    public AudioSource _audio;
    public BoxCollider2D _head;
    public CircleCollider2D _mouth;
    
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(_speed, 0);
        _audio.clip = _enter;
        _audio.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && !(player is null))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _head.enabled = false;
            _audio.clip = _eat;
            _audio.Play();
            Debug.Log("SNAKE ATE TOAD!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && !(player is null))
        {
            _mouth.enabled = false;
            Die();
        }
    }

    private void Die()
    {
        _audio.clip = _die;
        _audio.Play();
        Destroy(gameObject, _audio.clip.length);
    }
}
