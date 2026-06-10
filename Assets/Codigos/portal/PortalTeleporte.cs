using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporte : MonoBehaviour
{
    [Tooltip("Nome exato da cena para onde o jogador será teletransportado")]
    public string nomeCenaDestino;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(nomeCenaDestino);
        }
    }
}

