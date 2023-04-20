using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

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
    private static string baseUrl = "http://localhost:3000";
    private string url;
    private System.Action<string, bool> callback;
    public static SPGApi instance;

    /**
     * When Defining the callback entry, theirs 2 required params, string result (define how to use it) and bool isSuccess (to define if it succeded)
     */
    public SPGApi(string url, System.Action<string, bool> callback)
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
        using (UnityWebRequest req = UnityWebRequest.Get(this.url))
        {
            yield return req.SendWebRequest();
            HandleResult(req);
        }
    }

    public IEnumerator Post(WWWForm body)
    {
        // WWWForm form = new WWWForm();
        // form.AddField("myField", "myData");

        using (UnityWebRequest req = UnityWebRequest.Post(url, body))
        {
            yield return req.SendWebRequest();
            HandleResult(req);
        }
    }

    public IEnumerator Put(byte[] body)
    {
        //byte[] body = System.Text.Encoding.UTF8.GetBytes("{\"name\":\"user_01\",\"address\":{\"raw\":\"MountFiji\"}}");

        using (UnityWebRequest req = UnityWebRequest.Put(this.url, body))
        {
            yield return req.SendWebRequest();
            HandleResult(req);
        }
    }

    public IEnumerator Delete()
    {
        using (UnityWebRequest req = UnityWebRequest.Delete(this.url))
        {
            yield return req.SendWebRequest();
            HandleResult(req);
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

    static public IEnumerator CheckPassword(string password, System.Action<string, bool> callback)
    {
        WWWForm body = new WWWForm();
        body.AddField("password", password);
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/password", callback);
        return api.Post(body);
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

    static public IEnumerator TestApi(System.Action<string, bool> callback)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/games/random", callback);
        return api.Get();
    }
}