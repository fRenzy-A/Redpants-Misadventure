using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public GameObject player;
    public Animator animator;

    public Queue<string> dialogue;


    void Start()
    {
        dialogue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences)
    {
        dialogue.Clear();
        dialogueUI.SetActive(true);

        SuspendPlayerControl();

        foreach (string currentLine in sentences)
        {
            dialogue.Enqueue(currentLine);
        }
        DisplayNextSentence();
    }

    void SuspendPlayerControl()
    {
        player.GetComponent<PlayerMovement_2D>().enabled = false;
        player.GetComponent<PlayerInteraction>().enabled = false;

        animator.SetFloat("Speed", 0);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Dialogue count " + dialogue.Count);
        if (dialogue.Count == 0)
        {
            EndDialogue();

            return;
        }
        string currentLine = dialogue.Dequeue();

        dialogueText.text = currentLine;
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        dialogue.Clear();

        player.GetComponent<PlayerMovement_2D>().enabled = true;
        player.GetComponent<PlayerInteraction>().enabled = true;
    }
}
