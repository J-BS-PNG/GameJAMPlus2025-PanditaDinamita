using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{

    public GameObject menu;
    public List<GameObject> StoryObjects;
    public int index;

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
        
    }

    public void StartGame()
    {
        // Logic to start the game, e.g., loading a new scene
        Debug.Log("Starting the game...");
        //menu.SetActive(false);
        SceneManager.LoadScene("Gloriplayground");
        //StoryObjects[index].SetActive(true);

    }

    /*void Update(){
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space key pressed, advancing story...");
            //AdvanceStory();
        }
    }*/


    void AdvanceStory()
    {
        if (index < StoryObjects.Count - 1)
        {
            StoryObjects[index].SetActive(false);
            index++;
            StoryObjects[index].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Gloriplayground");
        }
    }

}
