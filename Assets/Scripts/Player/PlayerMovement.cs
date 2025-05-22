using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("JoyStick")]
    [SerializeField] private DynamicJoystick joystick;

    private Vector3 _input;
    private int _idle = Animator.StringToHash("Idle");
    private int _blendID = Animator.StringToHash("Walk");

    [Header("Player")]
    [SerializeField] private GameObject model;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private void FixedUpdate()
    {
        _input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        float inputMagnitude = _input.magnitude;
        animator.SetFloat(_blendID, inputMagnitude);

        if (inputMagnitude > 0.1f)
        {
            Vector3 direction = _input.normalized;
            Vector3 move = direction * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + move);

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);

            animator.SetBool(_idle, false);
        }
        else
        {
            animator.SetBool(_idle, true);
        }
    }
    private void OnEnable()
    {
        //EventBroker.OnUpdateCostume += UpdateButtons;
    }

    private void OnDisable()
    {
        //EventBroker.OnUpdateCostume -= UpdateButtons;
    }
}
