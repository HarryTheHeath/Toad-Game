using Entity;
using Unity.VisualScripting;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float _speed = 2;
    public AudioClip _enter;
    public AudioClip _die;
    public AudioClip _eat;
    public AudioSource _audio;
    
    private Rigidbody2D _rigidbody2D;
    private PlayerController _playerController;

    private void Start()
    {
        
        if (transform.position.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            _speed = 0-_speed;
        }
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(_speed, 0);

        if (_enter != null)
        {
            _audio.clip = _enter;
            _audio.Play();
        }
    }

    
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
            return;
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        
        if (player != null && !(player is null))
        {
            _rigidbody2D.velocity = Vector2.zero;

            if (_eat != null)
            {
                _audio.clip = _eat;
                _audio.Play();
            }
            Debug.Log("MUSHROOM ATE TOAD!");
        }
    }
    
    public void Die()
    {
        if (_die != null)
        {
            _audio.clip = _die;
            _audio.Play();
        }

        Destroy((GetComponent<Animator>()));
        
        foreach (Transform child in transform)
        {
            Rigidbody2D r = child.AddComponent<Rigidbody2D>();
            r.velocity = new Vector2(Random.Range(-2,-5),Random.Range(5,8));
        }
        
        Destroy(gameObject, _audio.clip.length);
    }
}
