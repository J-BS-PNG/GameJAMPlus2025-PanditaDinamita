using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    public string DialogueStart = "This is the mission beginning of the dialogue.";
    public string DialogueEnd = "This is the mission end of the dialogue.";
    public Movement player;

    public GameObject dialogueBubblePrefab;
    public Canvas uiCanvas;
    public float bubbleDuration = 3f;

    private GameObject activeBubble;

    void Start()
    {
        player.objectObtained = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.objectObtained)
            {
                // Start the dialogue
                Debug.Log("NPC: " + DialogueEnd);
                ShowDialogue(DialogueEnd);
            }
            else
            {
                // Start the dialogue
                Debug.Log("NPC: " + DialogueStart);
                ShowDialogue(DialogueStart);
            }
        }
    }

    void ShowDialogue(string message)
    {
        if (activeBubble != null)
            Destroy(activeBubble);

        activeBubble = Instantiate(dialogueBubblePrefab, uiCanvas.transform);

        TMP_Text textComponent = activeBubble.GetComponentInChildren<TMP_Text>();
        RectTransform bubbleRect = activeBubble.GetComponent<RectTransform>();

        if (textComponent != null)
        {
            textComponent.text = message;
            LayoutRebuilder.ForceRebuildLayoutImmediate(bubbleRect);

            Vector2 newSize = new Vector2(
                textComponent.preferredWidth + 40f,  // margen horizontal
                textComponent.preferredHeight + 20f  // margen vertical
            );

            bubbleRect.sizeDelta = newSize;
        }

        Vector3 worldPos = transform.position + Vector3.up * 2f;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        activeBubble.transform.position = screenPos;

        Destroy(activeBubble, bubbleDuration);
    }
}