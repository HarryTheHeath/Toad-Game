using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity
{
    public class Health : MonoBehaviour, IDamageable
    {
        public Action<int> OnHealthChanged;
        [SerializeField]
        private int _health;
        
        [SerializeField]
        [Tooltip("The amount of time the entity is invulnerable after being hit")]
        private float _invulnerabilityTime;
        private bool _invulnerable;

        public bool IsDead { get; set; }
        
        
        public int CurrentHealth
        {
            get => _health;
            set => _health = value;
        }
        
        private void Start()
        {
            CurrentHealth = _health;
            OnHealthChanged?.Invoke(CurrentHealth);
            
            if (CompareTag("Player"))
                GameObject.Find("PlayerHealth").GetComponent<TMPro.TextMeshProUGUI>().text = _health.ToString();
        }

        public void ModifyHealth(int healthValueChange)
        {
            if (_invulnerable && healthValueChange <= 0)
                    return;  
                
            CurrentHealth += healthValueChange;
            OnHealthChanged?.Invoke(CurrentHealth);
            
            if (CompareTag("Player"))
                GameObject.Find("PlayerHealth").GetComponent<TMPro.TextMeshProUGUI>().text = _health.ToString();


            if (!_invulnerable && healthValueChange <= 0)
            {
                StartCoroutine(InvulnFrameTimer(_invulnerabilityTime));

                if (CurrentHealth > 0){
                    //_animator.SetTrigger(Animator.StringToHash("TakeDamage"));
                }
                
            }
            
            if (CurrentHealth <= 0)
            {
                //_animator.SetTrigger(Animator.StringToHash("Dead"));
                StartCoroutine(StartDeath());
            }

        }

        private IEnumerator StartDeath()
        {
            yield return new WaitForSeconds(0f);
            OnDeath();
        }
        
        private void OnDeath() 
        {
            
            Destroy((GetComponent<Animator>()));
        
            foreach (Transform child in transform)
            {
                Rigidbody2D r = child.AddComponent<Rigidbody2D>();
                r.velocity = new Vector2(Random.Range(-5,5),Random.Range(5,8));
                r.angularVelocity = Random.Range(-360f, 360f);
                SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
                if (sprite != null && !(sprite is null))
                {
                    StartCoroutine(FadeOut(sprite, 1));
                }
            }
            transform.DetachChildren();

            if (CompareTag("Player"))
                GetComponent<BoxCollider2D>().enabled = false;

            IsDead = true;
        }
        
        private IEnumerator FadeOut(SpriteRenderer sprite, float fadeDuration)
        {
            float elapsedTime = 0f;
            Color startColor = sprite.color;

            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                sprite.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Make sure alpha is set to zero at the end of the fade
            sprite.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        }

        private IEnumerator InvulnFrameTimer(float invulnFrameTimer)
        {
            _invulnerable = true;
            yield return new WaitForSeconds(invulnFrameTimer);
            _invulnerable = false;
        }
    }
}