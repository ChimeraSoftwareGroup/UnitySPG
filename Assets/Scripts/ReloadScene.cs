using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    //5 sneuk / 7 bobee
  public void ScriptA()
    {
       // SceneManager.UnloadScene(7);
        SceneManager.LoadScene(5);

    }
    public void ScriptB()
    {
        SceneManager.UnloadScene(5);
        SceneManager.LoadScene(7);
    }
}
