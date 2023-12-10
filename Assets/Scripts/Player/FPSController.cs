using UnityEngine;
using TMPro;

public class FPSController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCameraTransform;
    public Transform gunTransform;
    public GameObject muzzleFlashPrefab;
    public GameObject bloodSplatterPrefab;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;

    // Ammunition and Reloading
    public int maxAmmoInMagazine = 12;
    private int currentMagazine;
    private int spareAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    // UI
    public TextMeshProUGUI ammoText;

    public EnemyAI enemy;

    void Start()
    {
        playerCamera = playerCameraTransform.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize ammo
        currentMagazine = maxAmmoInMagazine;
        spareAmmo = 60; // Set your total ammo here or load it from another source
        UpdateAmmoUI();

        characterController = GetComponent<CharacterController>(); // Add this line
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
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            if (currentMagazine > 0)
            {
                Shoot();
            }
            else
            {
                Reload();
            }
        }

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentMagazine < maxAmmoInMagazine)
        {
            Reload();
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
            Health enemyHealth = hit.transform.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10);

                // Instantiate Blood Splatter at the hit point
                GameObject bloodSplatter = Instantiate(bloodSplatterPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                // Destroy the blood splatter after half a second
                Destroy(bloodSplatter, 0.5f);
            }
        }

        // Play Shoot Sound
        GetComponent<AudioSource>().PlayOneShot(shootSound);

        // Decrease ammo count
        currentMagazine--;
        UpdateAmmoUI();
    }

    void Reload()
    {
        if (currentMagazine < maxAmmoInMagazine && spareAmmo > 0 && !isReloading)
        {
            // Calculate the remaining ammo needed to fill the magazine
            int ammoNeeded = maxAmmoInMagazine - currentMagazine;

            // Calculate how much ammo to reload (either the remaining ammo or the spare ammo, whichever is smaller)
            int ammoToReload = Mathf.Min(ammoNeeded, spareAmmo);

            // Subtract the reloaded ammo from the spare ammo
            spareAmmo -= ammoToReload;

            // Play Reload Sound
            GetComponent<AudioSource>().PlayOneShot(reloadSound);

            // Set reloading flag
            isReloading = true;

            // Invoke the FinishReloading function after the reloadTime
            Invoke("FinishReloading", reloadTime);

            // Set the current magazine to the reloaded amount
            currentMagazine = ammoToReload;

            // Update the UI
            UpdateAmmoUI();
        }
    }

    void FinishReloading()
    {
        // Reset the reloading flag
        isReloading = false;

        // Update the UI
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentMagazine + " / " + spareAmmo;
        }
    }
}
