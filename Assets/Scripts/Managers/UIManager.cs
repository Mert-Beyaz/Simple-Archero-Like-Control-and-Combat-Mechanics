using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject tutorial;


    private void Awake()
    {
        Instance = this;
        EventBroker.OnFirstTouch += OnFirstTouch;
    }
    private void OnFirstTouch()
    {
        tutorial.SetActive(false);
    }

    private void OnDisable()
    {
        EventBroker.OnFirstTouch -= OnFirstTouch;
    }
}
