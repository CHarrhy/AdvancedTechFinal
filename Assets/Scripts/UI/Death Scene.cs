using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DeathScene : MonoBehaviour
{
    public Image overlay;
    public TextMeshProUGUI deathMessage;
    public Button retryButton;
    public Button mainMenuButton;
    public Button quitButton;
    public AudioSource audioSource; // Reference to the AudioSource component


    private bool isMouseVisible = false;

    void Start()
    {
        // Set up button click events
        retryButton.onClick.AddListener(Retry);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Set initial visibility
        SetOverlayVisibility(true);
        SetDeathMessage("YOU DIED!");

        // Hide the mouse cursor initially
        Cursor.visible = false;

        // Show the mouse cursor when the scene starts
        ToggleMouseVisibility(true);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        HandleMouseVisibility();

        // Add other necessary update logic here
    }

    void Retry()
    {
        // Restart the main gameplay scene
        SceneManager.LoadScene("Main"); // Replace "YourMainGameplayScene" with your actual main gameplay scene name
    }

    void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu"); // Replace "MainMenu" with your actual main menu scene name
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void SetOverlayVisibility(bool isVisible)
    {
        overlay.gameObject.SetActive(isVisible);
    }

    void SetDeathMessage(string message)
    {
        deathMessage.text = message;
    }

    void HandleMouseVisibility()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMouseVisibility(!isMouseVisible);
        }
    }

    void ToggleMouseVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
        isMouseVisible = isVisible;

        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            EventSystem.current.SetSelectedGameObject(retryButton.gameObject);
        }
    }
}
