using System;
using UnityEngine;

public enum GameState { None,Playing,Waiting,Finish }
public class GameManager : Singleton<GameManager>
{
    public GameState GameState;

    private void Start()
    {
        ChangeToGameState(GameState.None);
    }
    internal void ChangeToGameState(GameState canGameState)
    {
        GameState = canGameState;
    }
}
