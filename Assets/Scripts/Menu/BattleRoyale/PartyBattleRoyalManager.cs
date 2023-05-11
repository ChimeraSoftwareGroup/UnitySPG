using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class PartyBattleRoyalManager : MonoBehaviour
{
    [SerializeField] GameManagerBR _gameManager;
    [SerializeField] NetworkManager _networkManager;

    [Header("Page GO")]
    public bool isHosting;
    [SerializeField] GameObject _hostCanvas;
    [SerializeField] GameObject _choiceNbMiniGame;
    [SerializeField] GameObject _joinCanvas;
    [SerializeField] GameObject _battleRoyaleChoice;
    [SerializeField] GameObject _errorPage;
    [SerializeField] GameObject _errorNbGame;
    [SerializeField] GameObject _errorCodeRoom;
    [SerializeField] GameObject _waitingPage;
    [SerializeField] Text _codeRoomHost;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonSound;
    public AudioClip errorSound;
    public AudioClip pageSound;

    [Header("Settings")]
    private string numberOfGames;
    private int _nbMiniGames;
    private string codeRoom;
    private int codeToJoin;
    public bool isCodeExist;

    private int _minIdGame = 7;
    private int _maxIdGame = 14;

    private int nbPlayerRoom = 0;

    private List<string> gameIdList;
    private Room currentRoom;

    #region Setters

    /**
     * Juste pour dire, c'est pas des "reading" ces fonctions, c'est des "writting"
     * Quand on lit un fichier/variable, on s'attend à recevoir des infos (les informations qu'on vient de lire), 
     * donc une fonction avec un return (un getter en somme), et qui, pour la plupart des cas, ne demande aucun argument
     * Quand on écrit un fichier/variable, c'est qu'on veut la stocker, l'enregistrer pour l'utiliser plus tard
     * donc une fonction void qui ne return rien, juste qui affilit une variable en argument à une variable physique
     * 
     * Si tu le sais my bad '--
     * C'est que je vois juste le résultat actuel dans le code
     * 
     * On pourrait dire que je chipote, mais c'est important d'avoir une manière de nommer similaire quand on travaille en groupe
     * et donc d'avoir une même base quand on cherche à nommer un truc
     */
    public void ReadingNumberOfGames(string _numberOfGames)
    {
        numberOfGames = _numberOfGames;
    }
    public void ReadingCodeRoom(string _codeRoom)
    {
        codeRoom = _codeRoom;
    }
    #endregion

    #region
    public Room getRoom()
    {
        return currentRoom;
    }
    #endregion

    #region show/hide canvas
    public void EnterInBattleRoyalMode(bool isHost)
    {
        isHosting = isHost;

        audioSource.PlayOneShot(buttonSound);

        _joinCanvas.SetActive(!isHosting);
        _choiceNbMiniGame.SetActive(isHosting);
    }
    public void CloseCodeError()
    {
        audioSource.PlayOneShot(pageSound);
        _errorCodeRoom.SetActive(false);
    }

    public void CloseBattleRoyale(bool sendDataToServer = true)
    {
        audioSource.PlayOneShot(pageSound);
        Debug.Log("Return to main menu");
        SceneManager.LoadScene("MenueScene");
        if (_networkManager.IsSocketStart() && sendDataToServer) _networkManager.SendQuittingRoom();
    }
    public void GoBackChoiceBattleRoyaleRole()
    {
        // Faire en sorte que l room cr��e s'il y en a une, se supprime
        _battleRoyaleChoice.SetActive(true);
        _hostCanvas.SetActive(false);
        audioSource.PlayOneShot(pageSound);
        _joinCanvas.SetActive(false);
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
    public void joinRoom()
    {
        if(codeRoom == null)
        {
            audioSource.PlayOneShot(errorSound);

            _errorCodeRoom.SetActive(true);
            return;
        }

        print("codeToJoin : " + codeRoom);
        StartCoroutine(SPGApi.CheckPassword(codeRoom, (response, isSuccess) => {
            if (!isSuccess)
            {
                audioSource.PlayOneShot(errorSound);
                _errorCodeRoom.SetActive(true);
                return;
            }

            currentRoom = JsonUtility.FromJson<Room>(response);
            _networkManager.StartSocket();

            audioSource.PlayOneShot(buttonSound);
            print("FIGHT ! ");
            _waitingPage.SetActive(true);
        }));
    }

    public void ShowCodeForHosting()
    {
        
        if(numberOfGames == null || numberOfGames == "0" || numberOfGames == "00" || numberOfGames == " ")
        {
            audioSource.PlayOneShot(errorSound);
            _errorNbGame.SetActive(true);
        }
        else
        {
            StartCoroutine(SPGApi.CreateRoom("Room", int.Parse(numberOfGames), _minIdGame, _maxIdGame, (response, isSuccess) => {

                if (!isSuccess) throw new Exception("Can't create Room");

                CreateRoomResponse json = JsonUtility.FromJson<CreateRoomResponse>(response);

                currentRoom = json.room.rows[0];
                gameIdList = json.gameList;
                _networkManager.StartSocket();

                _codeRoomHost.text = currentRoom.password;

                audioSource.PlayOneShot(buttonSound);
                _hostCanvas.SetActive(true);
                _choiceNbMiniGame.SetActive(false);
                audioSource.PlayOneShot(buttonSound);
            }));

            // Envoyer "_nbMiniGames" au back avec en plus min et max des ID des mini-jeu (cf variables)
            // Moulinette dans le back pour faire une liste entre id min et id max de la longueure de _nbMiniGames
            // --- Renvoie la liste � unity (print la liste) --- Via Socket
            // G�n�rer un code et le montrer � l'host (envoyer un int �a suffit + print)
        }
    }

    /*
     * Call in Unity (Fight Button for Host)
     */
    public void StartModeBRParty()
    {
        /*_nbMiniGames = int.Parse(numberOfGames);
        // Envoyer au back les param�tres choisis par l'host
        Debug.Log("Nombre de mini-jeux : " + _nbMiniGames);*/

        _networkManager.SendStartGame(gameIdList);

        // Ensuite, r�cup�rer le retour du back end.*
        // Envoyer la liste de jeu, ou la r�cup�rer c�t� GameManager.
    }

    public void AddOnePlayer()
    {
        nbPlayerRoom++;
    }

    public void RemoveOnePlayer()
    {
        nbPlayerRoom--;
    }
}
