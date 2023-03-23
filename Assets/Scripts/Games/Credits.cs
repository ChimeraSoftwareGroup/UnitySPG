using UnityEngine;

public class Credits : MonoBehaviour
{
    private void LoadMainMenu()
    {
        //message.SendMessageToFlutter("closeUnity");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }
}
