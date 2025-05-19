using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TestScript"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

