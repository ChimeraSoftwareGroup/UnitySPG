using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class SocketManager
{
    public SocketIOUnity socket;

    public SocketManager()
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

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected !");
            Debug.Log(sender);
            Debug.Log(e);
            //callback
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        ////

        Debug.Log("Connecting...");
        socket.Connect();

        socket.OnUnityThread("start game", (data) =>
        {
            //Need callback
        });

        socket.OnUnityThread("end game", (data) =>
        {
            //Need callback
        });

        socket.OnUnityThread("player join", (data) =>
        {
            //Need callback
        });

        socket.OnUnityThread("player quit", (data) =>
        {
            //Need callback
        });

        socket.OnUnityThread("delete room", (data) =>
        {
            //Need callback
        });
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

    public void EmitTest()
    {
        string txt = "sample text";

        socket.Emit("start game", txt);
    }

    public void EmitQuittingRoom()
    {
        socket.Emit("quit room", socket.Id);
    }

    /**
     * Used when no mini-games left and when the players loses
     */
    public void EmitEndGame(string data)
    {
        socket.Emit("ending game", data);
    }
}
