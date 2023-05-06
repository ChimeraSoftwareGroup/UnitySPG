using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ChoiceGameTraining : MonoBehaviour
{
    [SerializeField] GameManagerTraining _gameManagerTraining;
    public int _gameChoose;

    public void ChoiceGameId(int gameID)
    {
        _gameChoose = gameID;
        _gameManagerTraining.NewGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

    }
}
