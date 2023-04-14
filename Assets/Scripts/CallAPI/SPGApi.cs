using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
public class SPGApi
{
    private static string baseUrl = "";
    private string url;
    private System.Action<string, bool> callback;
    public static SPGApi instance;

    public SPGApi(string url, System.Action<string, bool> callback)
    {
        this.url = url;
        this.callback = callback;
    }


    #region Request Route
    private IEnumerator HandleRequest(UnityWebRequest req)
    {
        // Request and wait for the desired page.
        yield return req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.Success)
        {
            this.callback(req.downloadHandler.text, false);
        } else
        {
            this.callback(req.error, false);
        }
    }

    public IEnumerator Get()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(this.url))
        {
            return HandleRequest(req);
        }
    }

    public IEnumerator Post(WWWForm body)
    {
        // WWWForm form = new WWWForm();
        // form.AddField("myField", "myData");

        using (UnityWebRequest req = UnityWebRequest.Post(url, body))
        {
            return HandleRequest(req);
        }
    }

    public IEnumerator Put(byte[] body)
    {
        //byte[] body = System.Text.Encoding.UTF8.GetBytes("{\"name\":\"user_01\",\"address\":{\"raw\":\"MountFiji\"}}");

        using (UnityWebRequest req = UnityWebRequest.Put(this.url, body))
        {
            return HandleRequest(req);
        }
    }

    public IEnumerator Delete()
    {
        using (UnityWebRequest req = UnityWebRequest.Delete(this.url))
        {
            return HandleRequest(req);
        }
    }
    #endregion

    static public IEnumerator CreateRoom(string roomName, System.Action<string, bool> callback)
    {
        WWWForm body = new WWWForm();
        body.AddField("name", roomName);
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room", callback);
        return api.Post(body);
    }

    static public IEnumerator ModifyRoom(int idRoom, byte[] body, System.Action<string, bool> callback)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom, callback);
        return api.Put(body);
    } 

    static public IEnumerator DeleteRoom(int idRoom, System.Action<string, bool> callback)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom, callback);
        return api.Delete();
    }

    static public IEnumerator GetPlayerList(int idRoom, System.Action<string, bool> callback)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom + "/player", callback);
        return api.Get();
    }

    static public IEnumerator JoinRoom(string password, int idPlayer, System.Action<string, bool> callback)
    {
        WWWForm body = new WWWForm();
        body.AddField("password", password);
        body.AddField("id_player", idPlayer);
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/join", callback);
        return api.Post(body);
    }

    static public IEnumerator QuitRoom(int idRoom, int idPlayer, System.Action<string, bool> callback)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom + "/players/" + idPlayer + "/leave", callback);
        return api.Delete();
    }
}