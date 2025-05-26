
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _firstInput = false;
    private float _projectileFlightTime = 0.5f;

    public bool FirstInput { get => _firstInput; set => _firstInput = value; }
    public float ProjectileFlightTime { get => _projectileFlightTime; }

    private void Awake()
    {
        Instance = this;
        EventBroker.Subscribe("OnFirstTouch", OnFirstTouch);
    }

    private void OnFirstTouch()
    {
        _firstInput = true;
    }


    private void OnDisable()
    {
        EventBroker.UnSubscribe("OnFirstTouch", OnFirstTouch);
    }

}
