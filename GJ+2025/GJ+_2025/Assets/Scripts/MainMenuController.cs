using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Asegúrate de tener TextMeshPro en tu canvas
using System.Collections;


public class MainMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject menu;           // El menú principal
    public GameObject blackScreen;    // El panel negro
    public TextMeshProUGUI dialogText; // Texto donde se escriben los diálogos

    [Header("Dialog Settings")]
    [TextArea(2, 5)]
    public List<string> Dialogs;      // Lista de diálogos
    public float typingSpeed = 0.04f; // Velocidad del efecto de escritura

    private int index = 0;
    private bool isTyping = false;
    private bool canContinue = false;

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("Starting the game...");
        menu.SetActive(false);          // Oculta el menú
        blackScreen.SetActive(true);    // Muestra la pantalla negra
        StartCoroutine(PlayIntroDialog()); // Inicia los diálogos
    }

    void Update()
    {
        // Permite avanzar solo si el jugador presiona espacio o clic
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && canContinue && !isTyping)
        {
            NextDialog();
        }
    }

    IEnumerator PlayIntroDialog()
    {
        dialogText.text = "";
        index = 0;
        yield return StartCoroutine(TypeDialog(Dialogs[index]));
    }

    IEnumerator TypeDialog(string text)
    {
        isTyping = true;
        canContinue = false;
        dialogText.text = "";

        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        canContinue = true;
    }

    void NextDialog()
    {
        if (index < Dialogs.Count - 1)
        {
            index++;
            StartCoroutine(TypeDialog(Dialogs[index]));
        }
        else
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        Debug.Log("All dialogs finished. Loading next scene...");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SceneJosue"); // Cambia por el nombre de tu escena
    }
}
