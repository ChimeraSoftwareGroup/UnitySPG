using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] PartyBattleRoyalManager _PBRM;
    [SerializeField] GameManagerBR _gameManagerBR;

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

    private void ReadDataFromBackend()
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
    }
}
