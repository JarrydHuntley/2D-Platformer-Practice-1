using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    Canvas canvas;
    float lastPauseTime;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false; // Hide the menu
    }

    void Update()
    {
        // Check against last pause time to assure we don't immediately unpause
        if (Input.GetButtonDown("Pause") && lastPauseTime < Time.unscaledTime)
            Resume();
    }

    public void Pause()
    {
        lastPauseTime = Time.unscaledTime; // Remember when we last paused
        Time.timeScale = 0f; // Stop time
        canvas.enabled = true; // Reveal the menu
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Resume time
        canvas.enabled = false; // Hide the menu
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
