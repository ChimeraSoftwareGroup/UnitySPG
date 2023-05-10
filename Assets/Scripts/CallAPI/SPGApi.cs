using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;

/**
 * To Call Data from our API:
 * From any file that need data: 
 *  StartCoroutine(SPGAPI.<staticMethods>(data, callback));
 * 
 * It will be in the callback that we handle the result that we want.
 * Define callBack like this: 
 *  (string response, bool isSuccess) => { 
 *      //code here 
 *  }
 * If isSuccess == false, it means that the request failed, and response will not shoot a stringify json but the web error.
 * It need to be handle for each called request (for now, maybe it will change in the future).
 */

public class SPGApi
{
    private static readonly string baseUrl = "https://nodespg.osc-fr1.scalingo.io"; //http://localhost:3000
    private readonly string url;
    private readonly Action<string, bool> callback;

    /**
     * When Defining the callback entry, theirs 2 required params, string result (define how to use it) and bool isSuccess (to define if it succeded)
     */
    public SPGApi(string url, Action<string, bool> callback)
    {
        this.url = url;
        this.callback = callback;
    }

    #region Request Route
    private void HandleResult(UnityWebRequest req)
    {
        // Request and wait for the desired page.
        if (req.result == UnityWebRequest.Result.Success)
        {
            this.callback(req.downloadHandler.text, true);
        } else
        {
            this.callback(req.error, false);
        }
    }

    public IEnumerator Get()
    {
        using UnityWebRequest req = UnityWebRequest.Get(this.url);
        yield return req.SendWebRequest();
        HandleResult(req);
    }

    public IEnumerator Post(WWWForm body)
    {
        // WWWForm form = new WWWForm();
        // form.AddField("myField", "myData");

        using UnityWebRequest req = UnityWebRequest.Post(url, body);
        yield return req.SendWebRequest();
        HandleResult(req);
    }

    public IEnumerator Put(byte[] body)
    {
        //byte[] body = System.Text.Encoding.UTF8.GetBytes("{\"name\":\"user_01\",\"address\":{\"raw\":\"MountFiji\"}}");

        using UnityWebRequest req = UnityWebRequest.Put(this.url, body);
        yield return req.SendWebRequest();
        HandleResult(req);
    }

    public IEnumerator Delete()
    {
        using UnityWebRequest req = UnityWebRequest.Delete(this.url);
        yield return req.SendWebRequest();
        HandleResult(req);
    }
    #endregion

    static public IEnumerator CreateRoom(string roomName, int nbGame, int minIDGame, int maxIDGame, Action<string, bool> callback) 
    {
        WWWForm body = new();
        body.AddField("name", roomName);
        body.AddField("nbGame", nbGame);
        body.AddField("minIDGame", minIDGame);
        body.AddField("maxIDGame", maxIDGame);
        SPGApi api = new(baseUrl + "/rooms", callback);
        return api.Post(body);

        //Return the array of random games here
    }

    static public IEnumerator ModifyRoom(int idRoom, byte[] body, Action<string, bool> callback)
    {
        SPGApi api = new(baseUrl + "/rooms/" + idRoom, callback);
        return api.Put(body);
    } 

    static public IEnumerator DeleteRoom(int idRoom, Action<string, bool> callback)
    {
        SPGApi api = new(baseUrl + "/rooms/" + idRoom, callback);
        return api.Delete();
    }

    //--Deprecated
    static public IEnumerator GetPlayerList(int idRoom, Action<string, bool> callback)
    {
        SPGApi api = new(baseUrl + "/rooms/" + idRoom + "/player", callback);
        return api.Get();
    }

    static public IEnumerator CheckPassword(string password, Action<string, bool> callback)
    {
        WWWForm body = new();
        body.AddField("password", password);
        SPGApi api = new(baseUrl + "/rooms/password", callback);
        return api.Post(body);
    }

    //--Deprecated
    static public IEnumerator JoinRoom(string password, int idPlayer, Action<string, bool> callback)
    {
        WWWForm body = new();
        body.AddField("password", password);
        body.AddField("id_player", idPlayer);
        SPGApi api = new(baseUrl + "/rooms/join", callback);
        return api.Post(body);
    }

    //--Deprecated
    static public IEnumerator QuitRoom(int idRoom, int idPlayer, Action<string, bool> callback)
    {
        SPGApi api = new(baseUrl + "/rooms/" + idRoom + "/players/" + idPlayer + "/leave", callback);
        return api.Delete();
    }

    //TEST
    static public IEnumerator TestApi(Action<string, bool> callback)
    {
        SPGApi api = new(baseUrl + "/games/random", callback);
        return api.Get();
    }
}