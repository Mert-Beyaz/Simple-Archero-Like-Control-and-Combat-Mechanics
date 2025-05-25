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


    private bool _isAttackSpeed = false;
    private bool _isBounceDamage = false;
    private bool _isBurnDamage = false;
    private bool _isArrowMultiplication = false;
    private bool _isRageMode = false;

    private void Awake()
    {
        Instance = this;
        SetSubscriptions();
    }

    private void SetSubscriptions()
    {
        EventBroker.OnFirstTouch += OnFirstTouch;
        skillBtn.onClick.AddListener(SetSkillPanel);
        attackSpeedBtn.onClick.AddListener(AttackSpeed);
        bounceDamageBtn.onClick.AddListener(BounceDamage);
        burnDamageBtn.onClick.AddListener(BurnDamage);
        arrowMultiplicationBtn.onClick.AddListener(ArrowMultiplication);
        rageModeBtn.onClick.AddListener(RageMode);
    }

    private void SetUnsubscriptions()
    {
        EventBroker.OnFirstTouch -= OnFirstTouch;
        attackSpeedBtn.onClick.RemoveListener(AttackSpeed);
        bounceDamageBtn.onClick.RemoveListener(BounceDamage);
        burnDamageBtn.onClick.RemoveListener(BurnDamage);
        arrowMultiplicationBtn.onClick.RemoveListener(ArrowMultiplication);
        rageModeBtn.onClick.RemoveListener(RageMode);
    }

    private void SetSkillPanel()
    {
        skillPanel.SetActive(!skillPanel.activeSelf);
    }

    private void OnFirstTouch()
    {
        tutorial.SetActive(false);
    }

    private void AttackSpeed()
    {
        _isAttackSpeed = !_isAttackSpeed;
        Debug.Log("AttackSpeed = " + _isAttackSpeed);
        EventBroker.InvokeOnOnChangeSkill(typeof(AttackSpeedSkill), _isAttackSpeed);
    }  
    private void BounceDamage()
    {
        _isBounceDamage = !_isBounceDamage;
        Debug.Log("BounceDamage = " + _isBounceDamage);
        EventBroker.InvokeOnOnChangeSkill(typeof(BounceDamageSkill), _isBounceDamage);
    }
    private void BurnDamage()
    {
        _isBurnDamage = !_isBurnDamage;
        Debug.Log("BurnDamage = " + _isBurnDamage);
        EventBroker.InvokeOnOnChangeSkill(typeof(BurnDamageSkill), _isBurnDamage);
    }
    private void ArrowMultiplication()
    {
        _isArrowMultiplication = !_isArrowMultiplication;
        Debug.Log("ArrowMultiplication = " + _isArrowMultiplication);
        EventBroker.InvokeOnOnChangeSkill(typeof(ArrowMultiplicationSkill), _isArrowMultiplication);
    }
    private void RageMode()
    {
        _isRageMode = !_isRageMode;
        Debug.Log("ArrowMultiplication = " + _isRageMode);
        EventBroker.InvokeOnOnChangeSkill(typeof(RageModeSkill), _isRageMode);
    }



    private void OnDisable()
    {
        SetUnsubscriptions();
    }
}
