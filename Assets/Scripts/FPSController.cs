using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCameraTransform;
    public Transform gunTransform;
    public GameObject muzzleFlashPrefab;
    public GameObject bloodSplatterPrefab;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = playerCameraTransform.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Player Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
        characterController.Move(movement * speed * Time.deltaTime);

        // Player Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Update Gun Rotation (but keep fixed position)
        UpdateGunRotation();

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void UpdateGunRotation()
    {
        // Rotate gun to match player's rotation
        gunTransform.rotation = playerCameraTransform.rotation;
    }

    void Shoot()
    {
        // Shooting Logic (Raycasting, damage, etc.)
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Instantiate Muzzle Flash at the end of the gun barrel
            Vector3 endOfGunBarrel = gunTransform.position + gunTransform.forward * 1.0f;
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, endOfGunBarrel, gunTransform.rotation);
            Destroy(muzzleFlash, 0.5f);

            // Apply damage to the hit object
            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);

                // Check if the enemy is still alive
                if (health.IsAlive())
                {
                    // Instantiate Blood Splatter at the hit point
                    GameObject bloodSplatter = Instantiate(bloodSplatterPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                    // Destroy the blood splatter after half a second
                    Destroy(bloodSplatter, 0.5f);
                }
            }
        }
    }

}
