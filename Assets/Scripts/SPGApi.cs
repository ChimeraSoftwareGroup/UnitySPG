using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SPGApi : MonoBehaviour
{
    public void TestData()
    {
        Debug.Log("Start Call !");
        StartCoroutine(Get("https://pokeapi.co/api/v2/pokemon/ditto"));
    }

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

    private IEnumerator Get(string url)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            return HandleRequest(req);
        }
    }

    private IEnumerator Post(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest req = UnityWebRequest.Post(url, form))
        {
            return HandleRequest(req);
        }
    }

    private IEnumerator Put(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest req = UnityWebRequest.Put(url, form))
        {
            return HandleRequest(req);
        }
    }

    private IEnumerator Delete(string url)
    {
        using (UnityWebRequest req = UnityWebRequest.Delete(url))
        {
            return HandleRequest(req);
        }
    }
}