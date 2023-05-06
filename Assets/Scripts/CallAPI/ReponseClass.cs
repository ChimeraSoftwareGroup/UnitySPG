using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameResponse
{
    public ArrayList gameIdList;
}

public class EndingScoreResponse
{
    public int position;
    public Score userScore;
    public Score bestScore;
}

public class Score
{
    public int nbGamesPlayed;
    public int nbLifeLeft;
}

public class Room
{
    public int id;
    public int password;
}

public class CreateRoomResponse
{
    public Room room;
    public Array gameIdList;
}