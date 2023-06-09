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

    [Header("What item")]
    public string item;

    [Header("Simple info Message")]
    public string infoMessage;
    private TMP_Text infoText;

    [Header("Dialogue Text")]
    [TextArea]
    public string[] sentences;
    public string[] whenQuestIsDoneDialogue;

    [Header("Disappear Upon Mission Complete")]
    public bool willIDisappear;

    public PlayerMovement_2D playerScript;
    public DialogueManager dialogueManager;
    public GameObject infoPanel;
    public void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<TMP_Text>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement_2D>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        infoPanel = GameObject.Find("Info Panel");
    }

    public void Nothing()
    {
        Debug.LogWarning(this.gameObject.name + " has not type set");
    }

    public void Info()
    {
        StartCoroutine(ShowInfo(infoMessage, 3.5f));
        infoPanel.SetActive(true);
    }

    public void Pickup()
    {
        playerScript.inventory.Add(item);
        //Debug.Log("You picked up " + this.gameObject.name);
        this.gameObject.SetActive(false);
    }

    public void Dialogue()
    {
        if (playerScript.inventory.Contains(item))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(whenQuestIsDoneDialogue);
            if (willIDisappear)
            {
                if (dialogueManager.dialogue.Count == 0)
                {
                    this.gameObject.SetActive(false);
                }               
            }
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(sentences);
        }
    }

    IEnumerator ShowInfo(string message, float delay)
    {
        infoText.text = message;
        
        yield return new WaitForSeconds(delay);
        infoText.text = null;
    }
}
