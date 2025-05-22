using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("JoyStick")]
    [SerializeField] private DynamicJoystick joystick;

    [Header("Player")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;

    [Header("Combat")]
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private Transform firePoint;

    [Header("Managers")]
    [SerializeField] private TargetingSystem targetingSystem;
    [SerializeField] private SkillManager skillManager;


    private float attackTimer;
    private Vector3 _input;
    private int _shoot = Animator.StringToHash("Shoot");
    private int _idle = Animator.StringToHash("Idle");
    private int _blendID = Animator.StringToHash("Walk");

    private void Awake()
    {
        EventBroker.OnFirstTouch += OnFirstTouch;
        animator.SetBool(_idle, true);
    }

    private void OnFirstTouch()
    {
        animator.SetBool(_idle, false);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.FirstInput) EventBroker.InvokeOnFirstTouch();

        if (!GameManager.Instance.FirstInput)  return; 

        _input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        float inputMagnitude = _input.magnitude;
        if (inputMagnitude > 0.1f)
        {
            Move();
            animator.SetBool(_shoot, false);
            animator.SetFloat(_blendID, inputMagnitude);
            attackTimer = 0f; 
        }
        else
        {
            AttackIfReady();
            animator.SetBool(_shoot, true);
        }
    }

    void Move()
    {
        Vector3 direction = _input.normalized;
        Vector3 move = direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    void AttackIfReady()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval / skillManager.GetAttackSpeedMultiplier())
        {
            attackTimer = 0f;
            Transform target = targetingSystem.GetNearestTarget(transform.position);
            if (target != null)
            {
                ProjectileFactory.Create(firePoint.position, target.position, skillManager);
            }
        }
    }

    private void OnDisable()
    {
        EventBroker.OnFirstTouch -= OnFirstTouch;
    }
}
