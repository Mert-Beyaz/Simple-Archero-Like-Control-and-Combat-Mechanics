using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject skillPanel;

    [Header("BUTTON")]
    [SerializeField] private Button skillBtn;
    [SerializeField] private Button attackSpeedBtn;
    [SerializeField] private Button bounceDamageBtn;
    [SerializeField] private Button burnDamageBtn;
    [SerializeField] private Button arrowMultiplicationBtn;
    [SerializeField] private Button rageModeBtn;

    private void Awake()
    {
        Instance = this;
        SetSubscriptions();
    }

    private void SetSubscriptions()
    {
        EventBroker.Subscribe("OnFirstTouch", OnFirstTouch);
        skillBtn.onClick.AddListener(SetSkillPanel);
    }

    private void SetUnsubscriptions()
    {
        EventBroker.UnSubscribe("OnFirstTouch", OnFirstTouch);
        skillBtn.onClick.RemoveListener(SetSkillPanel);
    }

    private void SetSkillPanel()
    {
        skillPanel.SetActive(!skillPanel.activeSelf);
    }

    private void OnFirstTouch()
    {
        tutorial.SetActive(false);
    }

    private void OnDisable()
    {
        SetUnsubscriptions();
    }
}
