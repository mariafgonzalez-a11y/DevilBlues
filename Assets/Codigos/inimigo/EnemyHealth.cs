using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Morte")]
    public GameObject deathEffect;
    public float destroyDelay = 2f;

    private EnemyAI enemyAI;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
    }

    /// <summary>
    /// Causa dano ao inimigo.
    /// Exemplo: enemy.GetComponent<EnemyHealth>().TakeDamage(25f);
    /// </summary>
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} recebeu {amount} de dano. Vida: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0f)
            Die();
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

        Debug.Log($"{gameObject.name} morreu!");

        if (enemyAI != null)
            enemyAI.enabled = false;

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        foreach (var col in GetComponents<Collider>())
            col.enabled = false;

        Destroy(gameObject, destroyDelay);

       // Inimigo específico
if (gameObject.name == "inimigo 1")
{
    if (Objetivo.Instance != null)
        Objetivo.Instance.Completar();
}
else
{
    // Verifica se inimigo 2, 3 e 4 estão todos mortos
    GameObject i2 = GameObject.Find("inimigo 2");
    GameObject i3 = GameObject.Find("inimigo 3");
    GameObject i4 = GameObject.Find("inimigo 4");

    bool i2morto = i2 == null || i2.GetComponent<EnemyHealth>().isDead;
    bool i3morto = i3 == null || i3.GetComponent<EnemyHealth>().isDead;
    bool i4morto = i4 == null || i4.GetComponent<EnemyHealth>().isDead;

    if (i2morto && i3morto && i4morto)
        if (Objetivo.Instance != null)
            Objetivo.Instance.Completar();
}

}
    }

