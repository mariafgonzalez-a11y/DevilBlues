using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform pontoRespawn;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Respawnar()
    {
        if (pontoRespawn == null)
        {
            Debug.LogWarning("Ponto de respawn não foi definido!");
            return;
        }

        // Se o player usa CharacterController, precisa desligar antes de teleportar
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = pontoRespawn.position;
            transform.rotation = pontoRespawn.rotation;
            characterController.enabled = true;
        }
        else
        {
            transform.position = pontoRespawn.position;
            transform.rotation = pontoRespawn.rotation;
        }
    }
}