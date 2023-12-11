using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public Image overlay;
    public TextMeshProUGUI deathMessage;
    public Button retryButton;
    public Button mainMenuButton;
    public Button quitButton;

    void Start()
    {
        // Set up button click events
        retryButton.onClick.AddListener(Retry);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Set initial visibility
        SetOverlayVisibility(true);
        SetDeathMessage("YOU DIED!");

        // You can customize the death message and other properties as needed
    }

    void Retry()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
