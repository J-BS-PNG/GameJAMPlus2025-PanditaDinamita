using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{

    public void QuitGameEnding()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
        
    }

    public void MainMenuEnding(){
        Debug.Log("Game Main menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
