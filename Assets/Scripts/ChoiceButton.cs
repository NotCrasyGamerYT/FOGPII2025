using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    private DialogueManager manager;
    private Choice choiceData;
    private Button button;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        
        if (button != null)
        {
            // Set up the listener to call the OnSelected method when clicked
            button.onClick.AddListener(OnSelected);
        }
    }

    public void Setup(DialogueManager dialogueManager, Choice choice)
    {
        manager = dialogueManager;
        choiceData = choice;
        buttonText.text = choice.choiceText;
    }

    private void OnSelected()
    {
        // Tell the manager which choice was selected
        manager.OnChoiceSelected(choiceData);
    }
}