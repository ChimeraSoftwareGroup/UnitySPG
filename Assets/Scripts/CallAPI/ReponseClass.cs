using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StartGameResponse
{
    public List<string> gameIdList;

    public StartGameResponse(List<string> list)
    {
        gameIdList = list;
    }
}

[System.Serializable]
public class EndingScoreResponse
{
    public int user_position;
    //public Score userScore;
    //public Score bestScore;
}

[System.Serializable]
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

[System.Serializable]
public class RoomRequest
{
    public List<Room> rows;
    public int rowCount;
}

[System.Serializable]
public class Room
{
    public int id;
    public string password;
}

[System.Serializable]
public class CreateRoomResponse
{
    public RoomRequest room;
    public List<string> gameList;
}