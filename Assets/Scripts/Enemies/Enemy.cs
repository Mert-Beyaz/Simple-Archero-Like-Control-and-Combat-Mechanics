using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [SerializeField] private ParticleSystem spawnParticle;
    [SerializeField] private ParticleSystem burnParticle;

    [Header("HealtyBar")]
    [SerializeField] private Image healthUI;
    [SerializeField] private Image healtyEffectUI;

    private float _burnTimeRemaining = 0;
    private float _burnDamagePerSecond;
    private Tween _healtyBarTween;

    void OnEnable()
    {
        SetHealth();
        _burnTimeRemaining = 0;
        spawnParticle.Play();
    }

    public void SetHealth()
    {
        currentHealth = maxHealth;
        HealtyBarAnim();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealtyBarAnim();
        if (currentHealth <= 0)
        {
            EnemyManager.Instance.Respawn(this.gameObject);
        }
    }

    public void ApplyBurn(float dps, float duration)
    {
        _burnDamagePerSecond = dps;
        _burnTimeRemaining += duration;
    }

    void Update()
    {
        if (_burnTimeRemaining > 0)
        {
            float burnThisFrame = _burnDamagePerSecond * Time.deltaTime;
            TakeDamage(burnThisFrame);
            _burnTimeRemaining -= Time.deltaTime;
            if (!burnParticle.isPlaying) burnParticle.Play();
        }
        else burnParticle.Stop();
    }

    private void HealtyBarAnim()
    {
        _healtyBarTween?.Kill();
        float fillAmount = currentHealth / maxHealth;
        healthUI.fillAmount = fillAmount;
        _healtyBarTween = healtyEffectUI.DOFillAmount(fillAmount, 1);
    }

}
