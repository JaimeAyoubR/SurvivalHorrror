using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject DeathUI;
    public GameObject WinUI;
    public bool isPaused = false;

    private void OnEnable()
    {
        GameManager.winGame += WinGame;
        Enemy_Scriptv2.attackEvent += LoseGame;

        pauseUI = GameObject.FindGameObjectWithTag("PauseMenu");

        DeathUI = GameObject.FindGameObjectWithTag("DeathMenu");

        WinUI = GameObject.FindGameObjectWithTag("VictoryMenu");

        pauseUI.GetComponent<Canvas>().enabled = false;
        WinUI.GetComponent<Canvas>().enabled = false;
        DeathUI.GetComponent<Canvas>().enabled = false;
    }


    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void LoseGame(Transform aaaa)
    {
        if (DeathUI == null)
            DeathUI = GameObject.FindGameObjectWithTag("DeathMenu");
        Cursor.lockState = CursorLockMode.None;
        DeathUI.GetComponent<Canvas>().enabled = true;
    }

    public void WinGame()
    {
        Cursor.lockState = CursorLockMode.None;
        if (WinUI == null)
            WinUI = GameObject.FindGameObjectWithTag("VictoryMenu");
        WinUI.GetComponent<Canvas>().enabled = true;
    }

    public void Resume()
    {
        pauseUI.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseUI.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}