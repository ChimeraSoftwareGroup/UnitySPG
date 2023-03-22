using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public static DialogueManager instance;
    private Queue<string> sentences;
    public Animator animator;
    public GameObject dialogueUI;
    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scène");
            return;
        }

        instance = this;
        sentences = new Queue<string>();


    }

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }


}
