using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    private Health health;

    // UI
    public TextMeshProUGUI ammoText;

    public EnemyAI enemy;

    private bool isAiming = false;
    public float zoomFOV = 40f; // Adjust the field of view for aiming
    private float originalFOV; // Store the original field of view

    // Custom gravity variables
    public float forceGravity = 20f;
    private float gravity;

    // Other movement variables
    private bool isCrouching = false;
    private bool isSprinting = false;

    private PauseManager pauseManager;
    public GameObject pauseManagerObject;

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

        health = GetComponent<Health>();

        originalFOV = playerCamera.fieldOfView;

        pauseManager = pauseManagerObject.GetComponent<PauseManager>();
    }

    void Update()
    {
        if (pauseManager.isPaused)
            return;

        // Check if the character is grounded
        if (characterController.isGrounded)
        {
            // Apply custom gravity
            gravity = -forceGravity;

            // Handle other movement logic (e.g., walking, jumping, crouching, sprinting)
            HandleMovement();
        }
        else
        {
            // Apply regular gravity when not grounded
            gravity += Physics.gravity.y * Time.deltaTime;
        }

        // Apply vertical movement (including gravity)
        Vector3 verticalMovement = new Vector3(0, gravity, 0) * Time.deltaTime;
        characterController.Move(verticalMovement);

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

        HandleAiming();
    }

    void HandleAiming()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            isAiming = true;
            // Adjust camera field of view based on aiming state
            playerCamera.fieldOfView = zoomFOV;

            // Adjust gun position for ADS
            //gunTransform.localPosition = new Vector3(0f, -0.2f, 0.5f);
        }
        else if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            isAiming = false;
            // Reset camera field of view
            playerCamera.fieldOfView = originalFOV;

            // Reset gun position
           // gunTransform.localPosition = Vector3.zero;
        }
    }

    void HandleMovement()
    {
        // Player Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;

        // Placeholder code for crouching (you need to implement your crouch logic)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            // Adjust characterController height or other crouch-related logic
        }

        // Placeholder code for sprinting (you need to implement your sprint logic)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            // Adjust movement speed or other sprint-related logic
        }
        else
        {
            isSprinting = false;
        }

        // Adjust movement speed based on crouch and sprint
        float finalSpeed = isCrouching ? speed / 2f : (isSprinting ? speed * 1.5f : speed);

        characterController.Move(movement * finalSpeed * Time.deltaTime);

        // Player Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Update Gun Rotation (but keep fixed position)
        UpdateGunRotation();
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
            // Instantiate Muzzle Flash at the end of the gun barrel
            Vector3 endOfGunBarrel = gunTransform.position + gunTransform.forward * 1.0f;
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, endOfGunBarrel, gunTransform.rotation);
            Destroy(muzzleFlash, 0.5f);

            // Apply damage to the hit object
            Health enemyHealth = hit.transform.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20);

                // Instantiate Blood Splatter at the hit point
                GameObject bloodSplatter = Instantiate(bloodSplatterPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                // Destroy the blood splatter after half a second
                Destroy(bloodSplatter, 0.5f);
            }
        }

        // Play Shoot Sound
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogError("AudioSource or shootSound is not set!");
        }

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

    public void AddAmmo(int ammoToAdd)
    {
        // Add ammoToAdd to spareAmmo
        spareAmmo += ammoToAdd;

        // Update the UI or perform any other necessary actions
        UpdateAmmoUI();
    }

    public void Die()
    {
        SceneManager.LoadScene("Death Scene"); // Replace "DeathScene" with the name of your death scene
        Debug.Log("Die being called");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
