using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState = GameState.Idle_State;

    private void Awake()
    {
        Instance = this;

        EventBroker.OnPlay += OnPlay;
    }

    private void OnPlay()
    {
        GameState = GameState.Walk_State;
    }

}

public enum GameState
{
    Idle_State,
    Walk_State,
    Shoot_State
}
