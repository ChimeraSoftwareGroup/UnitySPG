using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SPGApi
{
    private static string baseUrl = "";
    private string url;

    public SPGApi(string url)
    {
        this.url = url;
    }

    #region Request Route
    private IEnumerator HandleRequest(UnityWebRequest req)
    {
        // Request and wait for the desired page.
        yield return req.SendWebRequest();

        switch (req.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + req.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + req.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + req.downloadHandler.text);
                break;
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

    static public IEnumerator CreateRoom(string roomName)
    {
        WWWForm body = new WWWForm();
        body.AddField("name", roomName);
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room");
        return api.Post(body);
    }

    static public IEnumerator ModifyRoom(int idRoom, byte[] data)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom);
        return api.Put(data);
    } 

    static public IEnumerator DeleteRoom(int idRoom)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom);
        return api.Delete();
    }

    static public IEnumerator GetPlayerList(int idRoom)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom + "/player");
        return api.Get();
    }

    static public IEnumerator JoinRoom(string password, int idPlayer)
    {
        WWWForm body = new WWWForm();
        body.AddField("password", password);
        body.AddField("id_player", idPlayer);
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/join");
        return api.Post(body);
    }

    static public IEnumerator QuitRoom(int idRoom, int idPlayer)
    {
        SPGApi api = new SPGApi(SPGApi.baseUrl + "/room/" + idRoom + "/players/" + idPlayer + "/leave");
        return api.Delete();
    }
}