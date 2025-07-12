using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 50f;

    private CharacterController controller;
    private float verticalRotation = 0f;
    private float shootCooldown = 0.5f;
    private float lastShotTime = -1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.SimpleMove(move);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        if (Input.GetMouseButtonDown(0) && Time.time > lastShotTime + shootCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletSpawn.forward * bulletSpeed;
            lastShotTime = Time.time;
        }
    }
}