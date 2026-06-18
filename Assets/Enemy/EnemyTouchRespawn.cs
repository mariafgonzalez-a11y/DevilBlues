using UnityEngine;

public class EnemyTouchRespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.Respawnar();
            }
            else
            {
                Debug.LogWarning("O Player não tem o script PlayerRespawn!");
            }
        }
    }
}