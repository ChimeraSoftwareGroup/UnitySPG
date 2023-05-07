using System;
using System.Collections;
using System.Collections.Generic;

public class StartGameResponse
{
    public List<string> gameIdList;
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

    public Score(int _nbGamesPlayed, int _nbLifeLeft)
    {
        nbGamesPlayed = _nbGamesPlayed;
        nbLifeLeft = _nbLifeLeft;
    }
}

public class RoomRequest
{
    public List<Room> rows;
    public int rowCount;
}

public class Room
{
    public int id;
    public string password;
}

public class CreateRoomResponse
{
    public RoomRequest room;
    public List<string> gameList;
}