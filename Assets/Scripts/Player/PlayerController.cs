using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    public Transform firePoint;
    public float attackInterval = 1f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float attackTimer;
    private TargetingSystem targetingSystem;
    private SkillManager skillManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetingSystem = FindObjectOfType<TargetingSystem>();
        skillManager = GetComponent<SkillManager>();
    }

    void Update()
    {
        moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (moveDirection.magnitude > 0.1f)
        {
            Move();
            attackTimer = 0f; // reset timer when moving
        }
        else
        {
            AttackIfReady();
        }
    }

    void Move()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;
        transform.forward = moveDirection.normalized;
    }

    void AttackIfReady()
    {
        rb.velocity = Vector3.zero;
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
}
