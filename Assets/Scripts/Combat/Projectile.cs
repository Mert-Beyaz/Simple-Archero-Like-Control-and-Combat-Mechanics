using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float gravity = 9.8f;
    public float damage = 10f;

    private Vector3 velocity;
    private Vector3 startPosition;
    private float time;
    private 

    void OnEnable()
    {
        startPosition = transform.position;
        StartCoroutine(Destroy());
    }

    public void Initialize(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    void Update()
    {
        time += Time.deltaTime;
        Vector3 displacement = velocity * time + 0.5f * Vector3.down * gravity * time * time;
        transform.position = startPosition + displacement;
        transform.forward = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StopCoroutine(Destroy());
            other.GetComponent<Enemy>().TakeDamage(damage);
            Pool.Instance.ReturnObject(PoolType.Projectile, gameObject);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        Pool.Instance.ReturnObject(PoolType.Projectile, gameObject);
    }

    public void SetBurnEffect(float dps, float duration)
    {
        //    this.burnDPS = dps;
        //    this.burnTimeRemaining = duration;
    }
}
