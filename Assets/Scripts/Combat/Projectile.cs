using System.Collections;
using UnityEngine;

namespace Archero
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        [SerializeField] private float lifetime = 5f;

        [SerializeField] private Rigidbody _rb;
        private Coroutine _destroyCoroutine;

        //Burn Effect
        private float _damagePerSecond = 0;
        private float _burnDuration = 0;
        //Bounce Effect
        private int _remainingBounces = 0;
        private bool _isBouncing = false;
        private Enemy _lastHitEnemy = null;

        private void OnEnable()
        {
            ResetProjectile();
        }

        public void Initialize(Vector3 launchVelocity)
        {
            _rb.velocity = launchVelocity;
            transform.forward = launchVelocity.normalized;
        }

        public Vector3 GetVelocity()
        {
            return _rb.velocity;
        }

        private void FixedUpdate()
        {
            if (_rb.velocity.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(_rb.velocity);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();
                if (enemy != null && enemy != _lastHitEnemy)
                {
                    enemy.TakeDamage(damage);
                    enemy.ApplyBurn(_damagePerSecond, _burnDuration);
                    _lastHitEnemy = enemy;

                    if (_isBouncing && _remainingBounces > 0)
                    {
                        _remainingBounces--;
                        Transform nextEnemy = TargetingSystem.Instance.GetNearestTarget(transform.position, true, enemy.gameObject);
                        if (nextEnemy != null)
                        {
                            Vector3 nextTarget = nextEnemy.position;
                            if (ProjectileFactory.CalculateParabolicVelocity(transform.position, nextTarget, out Vector3 velocity))
                            {
                                _rb.velocity = velocity;
                                transform.forward = velocity.normalized;
                                return;
                            }
                        }
                    }

                    Pool.Instance.ReturnObject(PoolType.Projectile, gameObject);
                }
            }
        }

        private IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(lifetime);
            Pool.Instance.ReturnObject(PoolType.Projectile, gameObject);
        }

        public void SetBurnEffect(float dps, float duration)
        {
            _damagePerSecond = dps;
            _burnDuration = duration;
        }

        public void SetBounceEffect(int bounceCount)
        {
            _remainingBounces = bounceCount;
            _isBouncing = true;
        }

        private void ResetProjectile()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.useGravity = true;

            _damagePerSecond = 0;
            _burnDuration = 0;

            _remainingBounces = 0;
            _isBouncing = false;
            _lastHitEnemy = null;

            if (_destroyCoroutine != null)
                StopCoroutine(_destroyCoroutine);
            _destroyCoroutine = StartCoroutine(DestroyAfterDelay());
        }
    }
}