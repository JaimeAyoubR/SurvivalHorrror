using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void OnDisable()
    {
        GameManager.winGame += Win;
    }

    void Win()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Map"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

