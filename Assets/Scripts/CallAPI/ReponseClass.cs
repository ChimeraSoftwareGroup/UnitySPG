using System;
using System.Collections;
using System.Collections.Generic;

public class StartGameResponse
{
    public List<int> gameIdList;
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
    public List<Room> arrayRows;
    public int rowCount;
}

public class Room
{
    public int id;
    public int password;
}

public class CreateRoomResponse
{
    public RoomRequest roomRequest;
    public List<int> gameIdList;
}