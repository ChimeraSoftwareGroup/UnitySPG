using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] PartyBattleRoyalManager _PBRM;
    [SerializeField] GameManagerBR _gameManagerBR;

    private SocketManager socket;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        // Connect to Backend.
        // Send first data to check if backend ok.
        // If backend not ok then not connected to net or server out => Get out from BR MODE.
    }

    // Update is called once per frame
    void Update()
    {
        // Listening to Backend.   
    }

    /*private void ReadDataFromBackend()
    {
        //data = {
        //    message: "PlayerDead",
        //    playerDeadName: "Lubain"
        //}

        //data = {
        //message: "StartGame",
        //    playerDeadName: "Lubain"
        //}

        //data = {
        //message: "GameOver",
        //    playerDeadName: "Lubain"
        //}

        //data = {
        //message: "RoomCodeCreated",
        //    roomCode: "QSUGDHJN"
        //}

        //if (data.message == "PlayerDead")
        //{
        //    data.playerDeadName;
        //}
        //if (data.message == "PlayerDead")
        //{
        //    data.playerDeadName;
        //}
        //if (data.message == "PlayerDead")
        //{
        //    data.playerDeadName;
        //}
        //if (data.message == "PlayerDead")
        //{
        //    data.playerDeadName;
        //}
        //if (data.message == "PlayerDead")
        //{
        //    data.playerDeadName;
        //}
    }
    public void CreateRoom(int numberOfGames)
    {
        NetworkJsonModel data = new NetworkJsonModel();
        data.message = "CreateNewRoom";
        //data.data = ""; // If not send it's undefined
        data.playerName = "Jacobu";
        data.isHost = true;
        BackendSendData(data);
        //Ask Backend to create room with the number of games required.
    }

    public void UpdateWaitingRoom()
    {
        // Update the list of string to display on the waiting room.
    }

    private void ShowCode()
    {
        // Call quand on reçoit le code depuis le backend.
        //PBRM._codeRoomHost = code
    }

    public void BackendSendData(NetworkJsonModel json)
    {
        JsonUtility.ToJson(json);
        // Send the Json to the backend
    }*/

    #region Socket
    public bool IsSocketStart()
    {
        return socket != null;
    }
    public void StartSocket()
    {
        socket = new SocketManager(
            OnConnect,
            OnStart,
            OnEnd,
            OnPlayerJoin,
            OnPlayerQuit,
            OnDeleteRoom,
            OnSendingData
        );
    }

    public SocketManager GetSocket()
    {
        return socket;
    }

    #region listenerers
    /**
     * Triggered when the connexion is completed
     */
    private void OnConnect()
    {
        //Get the actual ID of the room
        socket.InitJoinRoom(_PBRM.getRoom().id);
    }

    /**
     * Triggered when the game is about to start
     */
    private void OnStart(StartGameResponse data)
    {
        _gameManagerBR.StartBattleRoyale(data.gameIdList);
    }

    /**
     * Triggered when the game ends
     */
    private void OnEnd(EndingScoreResponse data)
    {
        Score userScore = data.userScore;
        Score bestScore = data.bestScore;

        //Need to display the score of player in Unity

        //Load EndingBR scene
    }

    /**
     * Triggered when a player join
     */
    private void OnPlayerJoin()
    {
        _PBRM.AddOnePlayer();
    }

    /**
     * Triggered when a player quit
     */
    private void OnPlayerQuit()
    {
        _PBRM.RemoveOnePlayer();
    }

    /**
     * Triggered when the room the player is in, is deleted from the server
     */
    private void OnDeleteRoom()
    {
        _PBRM.CloseBattleRoyale();
    }

    //If the current player didn't send his data yet
    private void OnSendingData()
    {
        //Need to test if the player has lost the party/already send the data
        // if not -> sendData()
        if (!_gameManagerBR.hasLost())
        {
            SendDataEndGame();
        }
    }
    #endregion

    public void SendDataEndGame()
    {
        //Need to get the number of Played game + PV Left
        Score sc = new(_gameManagerBR.GetGameFinished() , _gameManagerBR.GetCurrentHp());
        socket.EmitEndGame(sc); //Pass a json stringify
    }

    /*
     * Need to be executed when the player want to quit the current view (which is the inside a room one)
     */
    public void SendQuittingRoom()
    {
        socket.EmitQuittingRoom();
    }
    #endregion
}
