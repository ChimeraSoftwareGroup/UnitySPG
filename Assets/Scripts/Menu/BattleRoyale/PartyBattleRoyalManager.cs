using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartyBattleRoyalManager : MonoBehaviour
{
    [SerializeField] GameManagerBR _gameManager;
    [SerializeField] NetworkManager _networkManager;

    public bool isHosting = false;
    [SerializeField] GameObject _hostCanvas;
    [SerializeField] GameObject _choiceNbMiniGame;
    [SerializeField] GameObject _joinCanvas;
    [SerializeField] GameObject _battleRoyaleChoice;
    [SerializeField] GameObject _errorPage;
    [SerializeField] GameObject _errorNbGame;
    [SerializeField] GameObject _errorCodeRoom;
    [SerializeField] Text _codeRoomHost;

    public AudioSource audioSource;
    public AudioClip buttonSound;
    public AudioClip errorSound;
    public AudioClip pageSound;

    private string numberOfGames;
    private int _nbMiniGames;
    private string codeRoom;
    private int codeToJoin;
    public bool isCodeExist;

    private int _minIdGame = 6;
    private int _maxIdGame = 8;

    private SocketManager socket;

    #region Setters

    /**
     * Juste pour dire, c'est pas des "reading" ces fonctions, c'est des "writting"
     * Quand on lit un fichier/variable, on s'attend à recevoir des infos (les informations qu'on vient de lire), 
     * donc une fonction avec un return (un getter en somme), et qui, pour la plupart des cas, ne demande aucun argument
     * Quand on écrit un fichier/variable, c'est qu'on veut la stocker, l'enregistrer pour l'utiliser plus tard
     * donc une fonction void qui ne return rien, juste qui affilit une variable en argument à une variable physique
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
    
    #region show/hide canvas
    public void EnterInBattleRoyalMode(bool isHost)
    {
        isHosting = isHost;
        if (isHosting)
        {
            _choiceNbMiniGame.SetActive(true);
        }
        else
        {
            _hostCanvas.SetActive(false);
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
            startSocket();

            audioSource.PlayOneShot(buttonSound);
            print("FIGHT ! ");
            // Ajoute le joueur � la liste des joueurs dans la room
            // Envoyer un message depuis back : "Vous avez rejoint la room num�ro + "id room" "
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
            // Envoyer "_nbMiniGames" au back avec en plus min et max des ID des mini-jeu (cf variables)
            // Moulinette dans le back pour faire une liste entre id min et id max de la longueure de _nbMiniGames
            // Renvoie la liste � unity (print la liste)
            // G�n�rer un code et le montrer � l'host (envoyer un int �a suffit + print)
            audioSource.PlayOneShot(buttonSound);
            _codeRoomHost.text = "1234567890"; // variable du code
            _hostCanvas.SetActive(true);
            audioSource.PlayOneShot(buttonSound);
            _choiceNbMiniGame.SetActive(false);
        }
    }

    public void StartModeBRParty()
    {
        _nbMiniGames = int.Parse(numberOfGames);
        // Envoyer au back les param�tres choisis par l'host
        Debug.Log("Nombre de mini-jeux : " + _nbMiniGames);

        // Ensuite, r�cup�rer le retour du back end.*
        // Envoyer la liste de jeu, ou la r�cup�rer c�t� GameManager.
    }
    
    #region Socket
    private void startSocket()
    {
        socket = new SocketManager();
        //socket.EmitTest();
    }
    #endregion
}
