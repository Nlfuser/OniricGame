using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button pauseButton;
    public GameObject pauseMenu;
    private bool isPaused = false;


    void Start()
    {
        // Hide the pause menu initially
        pauseMenu.SetActive(false);

        // Add a listener to the UI button
        pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    void Update()
    {
        // You can still use Escape key for pausing if you want
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        // Pause or resume the game
        if (isPaused)
        {
            Time.timeScale = 0f; // Stop time to pause the game
            pauseMenu.SetActive(true); // Show the pause menu
        }
        else
        {
            Time.timeScale = 1f; // Resume time to unpause the game
            pauseMenu.SetActive(false); // Hide the pause menu
        }
    }

    // This method can be attached to the onClick event of your UI button
    public void OnPauseButtonClick()
    {
        TogglePause();
    }


}
