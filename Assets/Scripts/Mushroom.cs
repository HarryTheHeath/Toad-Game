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
    public int Damage = 1;

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
            other.gameObject.GetComponent<IDamageable>().ModifyHealth(-Damage);
            Die();
        }
    }
    
    public void Die()
    {
        if (_die != null)
        {
            _audio.clip = _die;
            _audio.Play();
            Destroy(gameObject, _audio.clip.length);

        }
        
        GetComponent<IDamageable>().ModifyHealth(-Damage);
    }
}
