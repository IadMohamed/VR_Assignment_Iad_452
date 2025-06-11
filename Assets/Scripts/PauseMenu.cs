using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour // Ensure the class name matches the file name!
{
    // Reference to the CanvasGroup component on your pause menu UI panel.
    public CanvasGroup pauseMenuCanvasGroup;

    // A boolean flag to keep track of the game's paused state.
    public bool isGamePaused = false;

    // The build index of your main menu scene. Set this in the Unity Inspector.
    // Make sure your scenes are added to File -> Build Settings...
    public int mainMenuSceneBuildIndex = 0; // Common for main menu to be index 0

    void Awake()
    {
        // Awake is called when the script instance is being loaded, even before Start().
        // This log will show the initial value of isGamePaused.
        Debug.Log("PauseMenu script Awake() called. isGamePaused at Awake: " + isGamePaused);
    }

    void Start()
    {
        // Start is called on the frame when a script is enabled.
        Debug.Log("PauseMenu script Start() called. isGamePaused at Start (initial value): " + isGamePaused);

        if (pauseMenuCanvasGroup != null)
        {
            SetCanvasGroupState(false); // Hide the pause menu initially
            Debug.Log("Pause menu UI initially hidden via CanvasGroup.");
        }
        else
        {
            Debug.LogError("ERROR: 'Pause Menu Canvas Group' is not assigned in the Inspector! Please drag your PauseMenu Panel (which needs a CanvasGroup component) onto this slot.");
        }
        Time.timeScale = 1f; // Ensure time is flowing normally at start
        isGamePaused = false; // Explicitly set to false at the beginning of the game
        Debug.Log("PauseMenu script Start() finished. isGamePaused now: " + isGamePaused + ", Time.timeScale: " + Time.timeScale);
    }

    void Update()
    {
        // This log is crucial for debugging. It should appear repeatedly in the Console.
        Debug.Log("Update() is running. isGamePaused: " + isGamePaused);

        // Check if the Escape key is pressed down.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed! Current isGamePaused: " + isGamePaused); // Log when the key is detected.

            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// Helper method to control the visibility and interactability of the CanvasGroup.
    /// </summary>
    /// <param name="isVisible">True to show the UI, false to hide it.</param>
    void SetCanvasGroupState(bool isVisible)
    {
        if (pauseMenuCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup is null, cannot set state.");
            return;
        }

        pauseMenuCanvasGroup.alpha = isVisible ? 1f : 0f; // Alpha 1 for visible, 0 for invisible
        pauseMenuCanvasGroup.interactable = isVisible;   // Enable/disable interaction
        pauseMenuCanvasGroup.blocksRaycasts = isVisible; // Block raycasts when visible
    }

    /// <summary>
    /// Method to pause the game.
    /// This should be called when the Escape key is pressed or a pause button is clicked.
    /// </summary>
    public void PauseGame()
    {
        Debug.Log("PauseGame() method called.");
        if (pauseMenuCanvasGroup != null)
        {
            SetCanvasGroupState(true); // Show the pause menu UI.
            Debug.Log("Pause menu UI set active (visible).");
        }
        Time.timeScale = 0f; // Stop time in the game, effectively pausing all game logic.
        isGamePaused = true; // Set the paused flag to true.
        Debug.Log("Game paused. Time.timeScale: " + Time.timeScale + ", isGamePaused: " + isGamePaused);
    }

    /// <summary>
    /// Method to resume the game.
    /// This should be called when the 'Resume' button is clicked.
    /// </summary>
    public void ResumeGame()
    {
        Debug.Log("ResumeGame() method called.");
        if (pauseMenuCanvasGroup != null)
        {
            SetCanvasGroupState(false); // Hide the pause menu UI.
            Debug.Log("Pause menu UI set inactive (hidden).");
        }
        Time.timeScale = 1f; // Set time back to normal, resuming game logic.
        isGamePaused = false; // Set the paused flag to false.
        Debug.Log("Game resumed. Time.timeScale: " + Time.timeScale + ", isGamePaused: " + isGamePaused);
    }

    /// <summary>
    /// Restarts the current scene by reloading its build index. Useful for a 'Restart' button.
    /// </summary>
    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("RestartGame() method called. Reloading current scene with index: " + currentSceneIndex);
        Time.timeScale = 1f; // Ensure time is flowing normally before loading
        SceneManager.LoadScene(currentSceneIndex); // Reload the current active scene by its build index.
    }

    /// <summary>
    /// Loads the main menu scene using its specified build index.
    /// Make sure the 'mainMenuSceneBuildIndex' is set correctly in the Inspector and the scene is in Build Settings.
    /// </summary>
    public void LoadMainMenu()
    {
        Debug.Log("LoadMainMenu() method called. Loading scene with index: " + mainMenuSceneBuildIndex);
        Time.timeScale = 1f; // Ensure time is flowing normally before loading
        SceneManager.LoadScene(mainMenuSceneBuildIndex); // Load the main menu scene by its build index.
    }

    /// <summary>
    /// Quits the application. This will only work in a built game, not in the Unity editor.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("QuitGame() method called. Application.Quit() invoked."); // Log for editor testing
        Application.Quit(); // Exit the application.
    }
}
