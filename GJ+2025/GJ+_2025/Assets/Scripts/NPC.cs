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
    public float bubbleDuration = 10f;

    private GameObject activeBubble;

    void Start()
    {
        player.objectObtained = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.objectObtained)
                ShowDialogue(DialogueEnd);
            else
                ShowDialogue(DialogueStart);
        }
    }

    public void ShowDialogue(string message)
    {
        if (activeBubble != null)
            Destroy(activeBubble);

        activeBubble = Instantiate(dialogueBubblePrefab, uiCanvas.transform);
        TMP_Text textComponent = activeBubble.GetComponentInChildren<TMP_Text>();
        RectTransform bubbleRect = activeBubble.GetComponent<RectTransform>();

        if (textComponent != null)
        {
            // Configuración visual
            textComponent.text = message;
            textComponent.enableWordWrapping = true;
            textComponent.alignment = TextAlignmentOptions.Center;
            textComponent.margin = new Vector4(20, 10, 20, 10);

            // 🔹 Establece un ancho máximo de texto para mantener forma rectangular
            float maxTextWidth = 380f;
            textComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxTextWidth);

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(bubbleRect);

            // 🔹 Calcula el tamaño de la burbuja ajustado al texto
            float minWidth = 150f;
            float maxWidth = 420f; // ligeramente más ancho
            float textWidth = Mathf.Clamp(textComponent.preferredWidth + 40f, minWidth, maxWidth);
            float textHeight = Mathf.Min(textComponent.preferredHeight + 30f, 220f); // altura máxima

            bubbleRect.sizeDelta = new Vector2(textWidth, textHeight);
        }

        // 🔹 Determinar de qué lado está el jugador
        bool playerIsLeft = player.transform.position.x < transform.position.x;

        // 🔹 Posición con offset en píxeles
        Vector3 worldPos = transform.position + Vector3.up * 2f;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        float pixelOffsetX = 1200f; // lado donde quieres que aparezca
        screenPos.x += playerIsLeft ? -pixelOffsetX : -pixelOffsetX;

        activeBubble.transform.position = screenPos;

        // 🔹 Animación de aparición
        bubbleRect.localScale = Vector3.zero;
        LeanTween.scale(bubbleRect, Vector3.one, 0.3f).setEaseOutBack();

        // 🔹 Animación de salida
        LeanTween.scale(bubbleRect, Vector3.zero, 0.25f)
            .setDelay(bubbleDuration - 0.25f)
            .setEaseInBack()
            .setOnComplete(() => Destroy(activeBubble));
    }

}

