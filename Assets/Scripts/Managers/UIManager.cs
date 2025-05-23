using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject SkillPanel;

    [Header("BUTTON")]
    [SerializeField] private Button SkillBtn;
    [SerializeField] private Button AttackSpeedBtn;
    [SerializeField] private Button BounceDamageBtn;
    [SerializeField] private Button BurnDamageBtn;
    [SerializeField] private Button ArrowMultiplicationBtn;


    private bool IsAttackSpeed = false;
    private bool IsBounceDamage = false;
    private bool IsBurnDamage = false;
    private bool IsArrowMultiplication = false;

    private void Awake()
    {
        Instance = this;
        SetSubscriptions();
    }

    private void SetSubscriptions()
    {
        EventBroker.OnFirstTouch += OnFirstTouch;
        SkillBtn.onClick.AddListener(SetSkillPanel);
        AttackSpeedBtn.onClick.AddListener(AttackSpeed);
        BounceDamageBtn.onClick.AddListener(BounceDamage);
        BurnDamageBtn.onClick.AddListener(BurnDamage);
        ArrowMultiplicationBtn.onClick.AddListener(ArrowMultiplication);
    }

    private void SetUnsubscriptions()
    {
        EventBroker.OnFirstTouch -= OnFirstTouch;
        AttackSpeedBtn.onClick.RemoveListener(AttackSpeed);
        BounceDamageBtn.onClick.RemoveListener(BounceDamage);
        BurnDamageBtn.onClick.RemoveListener(BurnDamage);
        ArrowMultiplicationBtn.onClick.RemoveListener(ArrowMultiplication);
    }

    private void SetSkillPanel()
    {
        SkillPanel.SetActive(!SkillPanel.activeSelf);
    }

    private void OnFirstTouch()
    {
        tutorial.SetActive(false);
    }

    private void AttackSpeed()
    {
        Debug.Log("AttackSpeed");
        IsAttackSpeed = !IsAttackSpeed;
        EventBroker.InvokeOnOnChangeSkill(typeof(AttackSpeedSkill), IsAttackSpeed);
    }  
    private void BounceDamage()
    {
        Debug.Log("BounceDamage");
        IsBounceDamage = !IsBounceDamage;
        EventBroker.InvokeOnOnChangeSkill(typeof(BounceDamageSkill), IsBounceDamage);
    }
    private void BurnDamage()
    {
        Debug.Log("BurnDamage");
        IsBurnDamage = !IsBurnDamage;
        EventBroker.InvokeOnOnChangeSkill(typeof(BurnDamageSkill), IsBurnDamage);
    }
    private void ArrowMultiplication()
    {
        Debug.Log("ArrowMultiplication");
        IsArrowMultiplication = !IsArrowMultiplication;
        EventBroker.InvokeOnOnChangeSkill(typeof(ArrowMultiplicationSkill), IsArrowMultiplication);
    }



    private void OnDisable()
    {
        SetUnsubscriptions();
    }
}
