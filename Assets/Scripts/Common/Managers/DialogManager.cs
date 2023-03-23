using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class DialogManager : MonoBehaviour
{
    [Header("Dialog Attributes")]
    [SerializeField] GameObject _dialogUI;
    [SerializeField] Text _dialogTitleUI;
    [SerializeField] Text _dialogTextUI;
    [SerializeField] Animator _animator;

    public static DialogManager instance;
    private Queue<string> sentences;

    private bool _firstTutorialDialogFinished = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scène");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();
       // StartTutorialDialog();

    }

    

    // Call by the Game Manager at the start of a new scene.
    public void StartTutorialDialog()
    {
        StartDialog(GetDialogFromScene());
    }

    private Dialog GetDialogFromScene()
    {
       return GameObject.FindObjectOfType<DialogInScene>().dialog;
    }
    private void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        _animator.SetBool("isOpen", true);
        _dialogTitleUI.text = dialog.title;
        foreach (string sentence in dialog.sentences)
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
        _dialogTextUI.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogTextUI.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void EndDialogue()
    {
        _animator.SetBool("isOpen", false);
        if(!_firstTutorialDialogFinished)
        {
             GameManager.instance.NewGame();
              //GameManager.instance.InputSettingsByPlayer();
            _firstTutorialDialogFinished = true;
        }
    }


}
