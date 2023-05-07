using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SocketManager
{
    public SocketIOUnity socket;

    public SocketManager(
        Action onConnect, 
        Action<StartGameResponse> onStart,
        Action<EndingScoreResponse> onEnd,
        Action onPlayerJoin,
        Action onPlayerQuit,
        Action onDeleteRoom,
        Action onSendingData)
    {
        //TODO: check the Uri if Valid.
        var uri = new Uri("http://localhost:3000");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY"}
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        #region reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected !");
            onConnect();
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        #endregion

        Debug.Log("Connecting...");
        socket.Connect();

        socket.OnUnityThread("start game", (data) =>
        {
            onStart(data.GetValue<StartGameResponse>());
        });

        socket.OnUnityThread("end game", (data) =>
        {
            onEnd(data.GetValue<EndingScoreResponse>());
        });

        socket.OnUnityThread("player join", (data) =>
        {
            onPlayerJoin();
        });

        socket.OnUnityThread("player quit", (data) =>
        {
            onPlayerQuit();
        });

        socket.OnUnityThread("delete room", (data) =>
        {
            onDeleteRoom();
        });

        socket.OnUnityThread("send last data", (data) =>
        {
            onSendingData();
        });
    }

    public void StartGame(List<string> array)
    {
        socket.Emit("start game", array);
    }

    public void EmitQuittingRoom()
    {
        socket.Emit("quit room");
    }

    /**
     * Used when no mini-games left and when the players loses
     */
    public void EmitEndGame(Score data)
    {
        socket.Emit("ending game", data);
    }

    public void InitJoinRoom(int idRoom)
    {
        socket.Emit("init join", idRoom);
    }

    public static bool IsJSON(string str)
    {
        if (string.IsNullOrWhiteSpace(str)) { return false; }
        str = str.Trim();
        if ((str.StartsWith("{") && str.EndsWith("}")) || //For object
            (str.StartsWith("[") && str.EndsWith("]"))) //For array
        {
            try
            {
                var obj = JToken.Parse(str);
                return true;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
