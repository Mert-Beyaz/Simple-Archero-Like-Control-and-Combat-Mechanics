using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float speed = 10f;

    private Rigidbody rb;
    private Coroutine destroyCoroutine;

    public float Speed { get => speed; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;

        if (destroyCoroutine != null)
            StopCoroutine(destroyCoroutine);
        destroyCoroutine = StartCoroutine(DestroyAfterDelay());
    }

    public void Initialize(Vector3 launchVelocity)
    {
        rb.velocity = launchVelocity;
        transform.forward = launchVelocity.normalized;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damage);
            Pool.Instance.ReturnObject(PoolType.Projectile, gameObject);
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Pool.Instance.ReturnObject(PoolType.Projectile, gameObject);
    }

    public void SetBurnEffect(float dps, float duration)
    {
        // Yanma efekti buraya
    }
}
