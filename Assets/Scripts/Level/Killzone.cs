using UnityEngine;
public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) => Destroy(col.gameObject);
}
