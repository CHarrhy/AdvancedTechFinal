using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class VictoryScene : MonoBehaviour
{
    public Image overlay;
    public TextMeshProUGUI victoryMessage;
    public Button playAgainButton;
    public Button mainMenuButton;
    public Button quitButton;

    private bool isMouseVisible = false;

    void Start()
    {
        // Set up button click events
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Set initial visibility
        SetOverlayVisibility(true);
        SetVictoryMessage("VICTORY!");

        // Hide the mouse cursor initially
        Cursor.visible = false;

        // Show the mouse cursor when the scene starts
        ToggleMouseVisibility(true);
    }

    void Update()
    {
        HandleMouseVisibility();

        // Add other necessary update logic here
    }

    void PlayAgain()
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

    void SetVictoryMessage(string message)
    {
        victoryMessage.text = message;
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
            EventSystem.current.SetSelectedGameObject(playAgainButton.gameObject);
        }
    }
}
