using Entity;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public AudioClip _enter;
    public AudioClip _die;
    public AudioClip _eat;
    public AudioSource _audio;
    public float _speed = 5f;
    public int Damage = 1;
    public Rigidbody2D _rigidbody2D;


    private void Start()
    {
        if (transform.position.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            _speed = 0-_speed;
        }
    }

    private void OnEnable()
    {
        if (_enter != null)
        {
            _audio.clip = _enter;
            _audio.Play();
        }
        
        Transform target = FindObjectOfType<PlayerController>().transform;

        // Calculate direction vector towards target
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        // Add velocity towards target
        _rigidbody2D.velocity = direction * _speed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.GetComponent<Health>().IsDead)
            return;

        if (other.gameObject.CompareTag("Enemy"))
            return;
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        
        if (player != null && !(player is null))
        {
            if (_eat != null)
            {
                _audio.clip = _eat;
                _audio.Play();
            }
            Debug.Log("BIRD ATE TOAD!");
            other.gameObject.GetComponent<IDamageable>().ModifyHealth(-Damage);

        }
    }
    
    private void Die()
    {
        if (_die != null)
        {
            _audio.clip = _die;
            _audio.Play();
            Destroy(gameObject, _audio.clip.length);
        }
    }
}
