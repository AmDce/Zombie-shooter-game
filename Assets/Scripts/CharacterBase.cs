using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterBase : MonoBehaviour
{
    public string CharacterName;
    public int MaxHealth;
    protected int currentHealth;
    public float AttackCooldown;
    public int Damage;

    public Image healthBarFill;

    protected virtual void Start()
    {
        currentHealth = MaxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / MaxHealth;
        }
    }

    public abstract void Attack();
    public abstract void Die();
}
