using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

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

    Transform _target = null;
    private float attackTimer;
    private float _animationSpeed = 1;
    private Vector3 _input;
    private int _shoot = Animator.StringToHash("Shoot");
    private int _attackSpeed = Animator.StringToHash("AttackSpeed");
    private int _idle = Animator.StringToHash("Idle");
    private int _blendID = Animator.StringToHash("Walk");

    private void Awake()
    {
        EventBroker.OnFirstTouch += OnFirstTouch;
        EventBroker.OnShoot += OnShoot;
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
            animator.SetBool(_idle, false);
            animator.SetFloat(_blendID, inputMagnitude);
            attackTimer = 0f;
        }
        else
        {
            AttackIfReady();
            animator.SetBool(_idle, true);
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
        attackTimer += Time.fixedDeltaTime;
        float attackSpeed = skillManager.GetAttackSpeedMultiplier();
        if (attackTimer >= attackInterval / attackSpeed)
        {
            animator.SetBool(_shoot, true);
            animator.SetFloat(_attackSpeed, _animationSpeed * attackSpeed);
            attackTimer = 0f;
            _target = targetingSystem.GetNearestTarget(transform.position);
            if (_target != null)
            {
                model.transform.DOLookAt(_target.position, 0.2f);
            }
        }
    }

    private void OnShoot()
    {
        ///////////////////////////////
        // Trajectory'yi çiz
        CalculateParabolicVelocity(firePoint.position, _target.position, 0.5f, out Vector3 velocity);
        trajectoryDrawer.DrawTrajectory(firePoint.position, velocity, Physics.gravity.magnitude);

        ///////////////////////////////
        ProjectileFactory.Create(firePoint.position, _target.position, skillManager);
    }

    private void OnDisable()
    {
        EventBroker.OnFirstTouch -= OnFirstTouch;
        EventBroker.OnShoot -= OnShoot;
    }


#if UNITY_EDITOR
    [SerializeField] private TrajectoryDrawer trajectoryDrawer;

    //public static bool CalculateBallisticVelocity(Vector3 start, Vector3 end, float speed, out Vector3 velocity)
    //{
    //    velocity = Vector3.zero;

    //    Vector3 toTarget = end - start;
    //    Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);
    //    float y = toTarget.y;
    //    float x = toTargetXZ.magnitude;
    //    float g = Physics.gravity.magnitude;

    //    float vSqr = speed * speed;
    //    float underRoot = vSqr * vSqr - g * (g * x * x + 2 * y * vSqr);

    //    if (underRoot < 0)
    //    {
    //        // Hedefe ulaşmak imkansız
    //        return false;
    //    }

    //    float root = Mathf.Sqrt(underRoot);
    //    float angleUp = Mathf.Atan((vSqr + root) / (g * x));
    //    // float angleDown = Mathf.Atan((vSqr - root) / (g * x)); // alternatif alçak atış

    //    Vector3 dir = toTargetXZ.normalized;
    //    Quaternion rot = Quaternion.AngleAxis(Mathf.Rad2Deg * angleUp, Vector3.Cross(dir, Vector3.up));
    //    velocity = rot * dir * speed;
    //    return true;
    //}

    //private static Vector3 CalculateVelocityToReachInTime(Vector3 start, Vector3 target, float time)
    //{
    //    Vector3 displacement = target - start;
    //    Vector3 horizontal = new Vector3(displacement.x, 0, displacement.z);
    //    float vertical = displacement.y;
    //    float gravity = Physics.gravity.y;

    //    Vector3 velocity = horizontal / time;
    //    velocity.y = (vertical - 0.5f * gravity * time * time) / time;

    //    return velocity;
    //}


    private static bool CalculateParabolicVelocity(Vector3 start, Vector3 end, float time, out Vector3 velocity)
    {
        velocity = Vector3.zero;

        Vector3 displacement = end - start;
        Vector3 horizontal = new Vector3(displacement.x, 0, displacement.z);
        float horizontalDistance = horizontal.magnitude;
        float vertical = displacement.y;

        Vector3 direction = horizontal.normalized;
        float g = -Physics.gravity.y;

        float vxz = horizontalDistance / time;
        float vy = (vertical + 0.5f * g * time * time) / time;

        velocity = direction * vxz;
        velocity.y = vy;

        return true;
    }
#endif

}
