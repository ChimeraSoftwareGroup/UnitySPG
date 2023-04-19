using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutoDialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public static TutoDialogueManager instance;
    private Queue<string> sentences;
    public Animator animator;
    public GameObject dialogueUI;


    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de TutoDialogueManager dans la scène");
            return;
        }

        instance = this;
        sentences = new Queue<string>();
    }
    private void Start()
    {
        Invoke("StartDialogue", 0f);
        Invoke("EndDialogue", 5f);

    }
    public void StartDialogueTuto(Dialog dialogue)
    {

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.title;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void EndDialogue()
    {
        print("Close dialogue");
        animator.SetBool("isOpen", false);
    }
}
