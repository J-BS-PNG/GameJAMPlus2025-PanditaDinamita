using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
        
    }

    public void MainMenu(){
        Debug.Log("Game Main menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
