using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Morte")]
    public GameObject deathEffect;
    public float restartDelay = 3f;

    [Header("UI de Morte")]
    public GameObject deathScreen;   // Arraste o painel DeathScreen aqui

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0f) Die();
    }

    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public float GetHealthPercent() => currentHealth / maxHealth;
    public bool IsDead() => isDead;

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (deathScreen != null)
            deathScreen.SetActive(true);

        Invoke(nameof(RestartScene), restartDelay);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
