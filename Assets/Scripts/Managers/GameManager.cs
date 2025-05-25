
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState = GameState.Idle_State;
    private bool _firstInput = false;

    private float _projectileFlightTime = 0.5f;

    public bool FirstInput { get => _firstInput; set => _firstInput = value; }
    public float ProjectileFlightTime { get => _projectileFlightTime; }

    private void Awake()
    {
        Instance = this;
        EventBroker.OnFirstTouch += OnFirstTouch;
    }

    private void OnPlay()
    {
        GameState = GameState.Walk_State;
    }

    private void OnFirstTouch()
    {
        _firstInput = true;
    }


  

}

public enum GameState
{
    Idle_State,
    Walk_State,
    Shoot_State
}
