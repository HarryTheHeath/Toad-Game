using System;
using System.Collections;
using UnityEngine;

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

        private bool IsDead { get; set; }
        
        
        public int CurrentHealth
        {
            get => _health;
            set => _health = value;
        }
        
        private void Start()
        {
            CurrentHealth = _health;
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void ModifyHealth(int healthValueChange)
        {
            if (_invulnerable && healthValueChange <= 0)
                    return;  
                
            CurrentHealth += healthValueChange;
            OnHealthChanged?.Invoke(CurrentHealth);


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
            yield return new WaitForSeconds(1.5f);
            OnDeath();
        }
        
        private void OnDeath() 
        {
            
            if (gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }


            if (gameObject.CompareTag("Player"))
            {
                
            }
            gameObject.SetActive(false);
            

            IsDead = true;
        }

        private IEnumerator InvulnFrameTimer(float invulnFrameTimer)
        {
            _invulnerable = true;
            var originalColor = GetComponent<SpriteRenderer>().color;             
            GetComponent<SpriteRenderer>().color = Color.red;             
            yield return new WaitForSeconds(invulnFrameTimer);
            GetComponent<SpriteRenderer>().color = originalColor;
            _invulnerable = false;
        }
    }
}