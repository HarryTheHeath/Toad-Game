using UnityEngine;

namespace Entity
{
    public class Attack : MonoBehaviour
    {
        private CircleCollider2D _circleCollider;
        public int Damage;


        void Awake() => _circleCollider = GetComponent<CircleCollider2D>();


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Enemy"))
                return;

            col.gameObject.GetComponent<IDamageable>().ModifyHealth(-Damage);
        }
    }
}
