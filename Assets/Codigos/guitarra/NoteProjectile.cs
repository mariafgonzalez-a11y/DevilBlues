using UnityEngine;

/// <summary>
/// Colocado automaticamente em cada projétil pelo GuitarShooter.
/// Causa dano ao inimigo ao colidir.
/// </summary>
public class NoteProjectile : MonoBehaviour
{
    public float damage = 25f;

    void OnCollisionEnter(Collision collision)
    {
        // Tenta causar dano — funciona em qualquer objeto com EnemyHealth
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"Nota acertou {collision.gameObject.name} por {damage} de dano!");
        }

        // Destrói o projétil ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
