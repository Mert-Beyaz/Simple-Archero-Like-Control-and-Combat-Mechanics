using UnityEngine;
using DG.Tweening;

namespace Archero
{
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
        [SerializeField] private AudioSource shootSound;

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
            EventBroker.Subscribe("OnFirstTouch", OnFirstTouch);
            EventBroker.Subscribe("OnShoot", OnShoot);
            animator.SetBool(_idle, true);
        }

        private void OnFirstTouch()
        {
            animator.SetBool(_idle, false);
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.Instance.FirstInput) EventBroker.Publish("OnFirstTouch");

            if (!GameManager.Instance.FirstInput) return;

            _input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            float inputMagnitude = _input.magnitude;
            if (inputMagnitude > 0.1f)
            {
                Move();
                animator.SetBool(_shoot, false);
                animator.SetBool(_idle, false);
                animator.SetFloat(_blendID, inputMagnitude);
                attackTimer = attackInterval;
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
            float attackSpeed = SkillManager.Instance.GetAttackSpeedMultiplier();
            if (attackTimer >= attackInterval / attackSpeed)
            {
                animator.SetBool(_shoot, true);
                animator.SetFloat(_attackSpeed, _animationSpeed * attackSpeed);
                attackTimer = 0f;
                _target = TargetingSystem.Instance.GetNearestTarget(transform.position);
                if (_target != null)
                {
                    model.transform.DOLookAt(_target.position, 0.2f);
                }
            }
        }

        private void OnShoot()
        {
            ProjectileFactory.Create(firePoint.position, _target.position);
            shootSound.Play();
        }

        private void OnDisable()
        {
            EventBroker.UnSubscribe("OnFirstTouch", OnFirstTouch);
            EventBroker.UnSubscribe("OnShoot", OnShoot);
        }
    }
}