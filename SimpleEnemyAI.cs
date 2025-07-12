using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float bulletSpeed = 30f;

    private float lastFireTime = 0;

    void Update()
    {
        if (player == null) return;

        transform.LookAt(player);
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Time.time > lastFireTime + fireRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
            lastFireTime = Time.time;
        }
    }
}