using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pauseScreen;
    
    public Button pauseExitButton;
    public Button pauseContinueButton;

    private void Awake()
    {
        pauseExitButton.onClick.AddListener(ExitGame);
        pauseContinueButton.onClick.AddListener(ContinueGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
    
    public void ExitGame()
    {
        Application.Quit();
    }
}