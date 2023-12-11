using UnityEngine;
using TMPro;

public class ObjectiveText : MonoBehaviour
{
    public float displayDuration = 5f; // Duration to display the objective text
    public string objectiveMessage = "Your objective message here";

    private void Start()
    {
        // Set the initial text and invoke a method to hide it after a delay
        SetObjectiveText(objectiveMessage);
        Invoke("HideObjectiveText", displayDuration);
    }

    private void SetObjectiveText(string text)
    {
        // Set the text of the TextMeshPro Text component
        GetComponent<TextMeshProUGUI>().text = text;
    }

    private void HideObjectiveText()
    {
        // Disable the TextMeshPro Text component to hide the objective text
        gameObject.SetActive(false);
    }
}
