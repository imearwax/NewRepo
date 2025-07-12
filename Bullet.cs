using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 5f;

    void Start()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Health>()?.TakeDamage(100);
        }

        Destroy(gameObject);
    }
}