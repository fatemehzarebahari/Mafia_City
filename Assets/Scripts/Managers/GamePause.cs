using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;
    private float savedTimeScale;
    [SerializeField] private GameObject pauseCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseCanvas.SetActive(true);
            savedTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = savedTimeScale;
            pauseCanvas.SetActive(false);
        }
    }

    public void Quit(){
        Application.Quit();
    }
}