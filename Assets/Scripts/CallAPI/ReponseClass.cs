using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameResponse
{
    public ArrayList gameIdList;
}

public class EndingScoreResponse
{
    public int userPosition;
    public Score userScore;
    public Score bestScore;
}

public class Score
{
    public int nbGamesPlayed;
    public int nbLifeLeft;
}