using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Camera deathCamera; // Reference to the death camera
    public Camera mainCamera; // Reference to the main camera
    public Image overlay;
    public TextMeshProUGUI deathMessage;
    public Button retryButton;
    public Button quitButton;
    public Button mainMenuButton;

    void Start()
    {
        // Initially hide the death screen UI
        SetDeathScreenVisibility(false);

        // Subscribe to the OnDeath event in the Health script
        Health playerHealth = FindObjectOfType<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath.AddListener(ShowDeathScreen);
        }

        // Set up button click events
        retryButton.onClick.AddListener(Retry);
        quitButton.onClick.AddListener(Quit);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    void ShowDeathScreen()
    {
        // Disable the main camera and enable the death camera
        mainCamera.gameObject.SetActive(false);
        deathCamera.gameObject.SetActive(true);

        // Set the death camera's position and rotation to match the main camera
        deathCamera.transform.position = mainCamera.transform.position;
        deathCamera.transform.rotation = mainCamera.transform.rotation;

        // Show the death screen UI with a red overlay
        SetDeathScreenVisibility(true);
        deathMessage.text = "You died!"; // Customize the death message if needed
    }

    void SetDeathScreenVisibility(bool isVisible)
    {
        overlay.gameObject.SetActive(isVisible);
        deathMessage.gameObject.SetActive(isVisible);
        retryButton.gameObject.SetActive(isVisible);
        quitButton.gameObject.SetActive(isVisible);
        mainMenuButton.gameObject.SetActive(isVisible);
    }

    void Retry()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Quit()
    {
        // Quit the application (works in the build, not in the editor)
        Application.Quit();
    }

    void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual main menu scene name
    }
}
