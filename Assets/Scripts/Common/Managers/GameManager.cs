using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isMiniGameFinished;
    public bool isPlayerHasWin;

    public virtual void Awake()
    {
        instance = this;
    }
    public abstract void NewGame();
    public abstract void GameObjectsActivationAtStartEatchGame();
    public abstract void LoseMiniGame();
    public abstract void GameOver();
    public abstract void NotificationPlayerAndSceneHasChanged(Player player);

    public abstract void setParametersOfCoopGame(
            List<string> nameOfPlayeList,
            bool isShuffleOn,
            float timeSelectedInSeconds,
            int numberOfMiniGamesSelected
        );
    public abstract void DisablePlayerAfterDamage();

}
