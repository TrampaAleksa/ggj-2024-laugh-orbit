using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pauseScreen;
    
    public Button pauseExitButton;
    public Button pauseContinueButton;

    public GameObject gameOverScreen;
    public Button gameOverRestartButton;
    public Button gameOverExitButton;

    private void Awake()
    {
        pauseExitButton.onClick.AddListener(ExitGame);
        pauseContinueButton.onClick.AddListener(ContinueGame);
        gameOverRestartButton.onClick.AddListener(RestartGame);
        gameOverExitButton.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverScreen.activeInHierarchy)
                return;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ContinueGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}