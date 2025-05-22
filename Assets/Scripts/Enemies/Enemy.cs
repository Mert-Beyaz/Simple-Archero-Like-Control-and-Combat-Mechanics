using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private float burnTimeRemaining;
    private float burnDPS;

    void OnEnable()
    {
        SetHealth();
    }

    public void SetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EnemyManager.Instance.Respawn(this);
        }
    }

    public void ApplyBurn(float dps, float duration)
    {
        burnDPS = dps;
        burnTimeRemaining = duration;
    }

    void Update()
    {
        if (burnTimeRemaining > 0)
        {
            float burnThisFrame = burnDPS * Time.deltaTime;
            TakeDamage(burnThisFrame);
            burnTimeRemaining -= Time.deltaTime;
        }
    }

}
