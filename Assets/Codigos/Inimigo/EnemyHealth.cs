using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 100;

    // A palavra 'public' é obrigatória para o outro script conseguir enxergar esta função
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Inimigo recebeu dano! Vida atual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Lógica para quando o inimigo morre (ex: destruir o objeto)
        Destroy(gameObject);
    }
}
