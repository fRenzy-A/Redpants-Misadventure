using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;


public class InteractionObject : MonoBehaviour
{
    public enum InteractableType
    {
        nothing,
        info,
        pickup,
        dialogue
    }

    [Header("Type of Interactable")]
    public InteractableType interType;

    [Header("Simple info Message")]
    public string infoMessage;
    private TMP_Text infoText;

    [Header("Dialogue Text")]
    [TextArea]
    public string[] sentences;
    public void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<TMP_Text>();
    }

    public void Nothing()
    {
        Debug.LogWarning(this.gameObject.name + " has not type set");
    }

    public void Info()
    {
        StartCoroutine(ShowInfo(infoMessage, 3.5f));
    }

    public void Pickup()
    {
        Debug.Log("You picked up " + this.gameObject.name);
        this.gameObject.SetActive(false);
    }

    public void Dialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(sentences);
    }

    IEnumerator ShowInfo(string message, float delay)
    {
        infoText.text = message;
        yield return new WaitForSeconds(delay);
        infoText.text = null;
    }
}
