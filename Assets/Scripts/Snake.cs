using UnityEngine;


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

        if (other.gameObject.CompareTag("Snake"))
            return;
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        
        if (player != null && !(player is null))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _head.enabled = false;

            if (_eat != null)
            {
                _audio.clip = _eat;
                _audio.Play();
            }
           
            Debug.Log("SNAKE ATE TOAD!");
        }
    }

    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
            return;
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && !(player is null))
        {
            _mouth.enabled = false;
            Die();
        }
    }

    
    
    private void Die()
    {
        if (_die != null)
        {
            _audio.clip = _die;
            _audio.Play();
        }
        
        Destroy(gameObject, _audio.clip.length);
    }
}
