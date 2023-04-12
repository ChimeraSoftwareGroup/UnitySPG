using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PartyBattleRoyalManager : MonoBehaviour
{

    public bool isHosting;
    [SerializeField] GameObject _hostCanvas;
    [SerializeField] GameObject _choiceNbMiniGame;
    [SerializeField] GameObject _joinCanvas;
    [SerializeField] GameObject _battleRoyaleChoice;
    [SerializeField] GameObject _errorPage;
    [SerializeField] GameObject _errorNbGame;
    [SerializeField] GameObject _errorCodeRoom;

    public AudioSource audioSource;
    public AudioClip buttonSound;
    public AudioClip errorSound;
    public AudioClip pageSound;

    private string numberOfGames;
    private int _nbMiniGames;
    private string codeRoom;
    private int codeToJoin;
    public bool isCodeExist;

    public void EnterInBattleRoyaleModeInHost()
    {
        isHosting = true;
        ChoiceRoleBattleRoyale();
    }
    public void EnterInBattleRoyaleModeInPlayer()
    {
        isHosting = false;
        ChoiceRoleBattleRoyale();
    }


    public void Update()
    {
       
    }

    #region Buttons Functions
    public void ChoiceRoleBattleRoyale()
    {
        if (isHosting)
        {
            _battleRoyaleChoice.SetActive(false);
            _joinCanvas.SetActive(false);
            audioSource.PlayOneShot(buttonSound);
            _choiceNbMiniGame.SetActive(true);

        }
        else
        {
            _battleRoyaleChoice.SetActive(false);
            _joinCanvas.SetActive(true);
            audioSource.PlayOneShot(buttonSound);
            _hostCanvas.SetActive(false);
        }
    }
    public void joinRoom()
    {
        print("Code room " + codeRoom);
       
        if(codeRoom == null)
        {
            audioSource.PlayOneShot(errorSound);

            _errorCodeRoom.SetActive(true);
        }
        else
        {
            codeToJoin = int.Parse(codeRoom);
            // Envoie au back du code pour savoir s'il existe : isCodeExiste == true si le code est bon
            print("codeToJoin : " + codeToJoin);

            if (isCodeExist == false)
            {
            audioSource.PlayOneShot(errorSound);
                _errorCodeRoom.SetActive(true);
            }
            else
            {
                audioSource.PlayOneShot(buttonSound);

                print("FIGHT ! ");
            }
        }
    }

    public void CloseCodeError()
    {
        audioSource.PlayOneShot(pageSound);
        _errorCodeRoom.SetActive(false);
    }
   
    public void CloseBattleRoyale()
    {
        audioSource.PlayOneShot(pageSound);
        SceneManager.LoadScene("MenueScene");
    }

    public void GoBackChoiceBattleRoyaleRole()
    {
        _battleRoyaleChoice.SetActive(true);
        _hostCanvas.SetActive(false);
        audioSource.PlayOneShot(pageSound);
        _joinCanvas.SetActive(false);
    }
    public void ShowCodeForHosting()
    {
        if(numberOfGames == null)
        {
            audioSource.PlayOneShot(errorSound);
            _errorNbGame.SetActive(true);
        }
        else
        {
            audioSource.PlayOneShot(buttonSound);

            _nbMiniGames = int.Parse(numberOfGames);
            _hostCanvas.SetActive(true);
            audioSource.PlayOneShot(buttonSound);
            _choiceNbMiniGame.SetActive(false);
        }
    }
    public void ShowError()
    {
        _errorPage.SetActive(true);
    }
    public void HideError()
    {
        _errorPage.SetActive(false);
    }

    public void CloseErrorNbGames()
    {
        _errorNbGame.SetActive(false);
    }

    #endregion

    public void ReadingNumberOfGames(string _numberOfGames)
    {
        numberOfGames = _numberOfGames;
    }
    public void ReadingCodeRoom(string _codeRoom)
    {
        codeRoom = _codeRoom;
    }
}
