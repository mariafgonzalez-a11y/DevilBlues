using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    [Tooltip("Nome ou índice da cena para onde o jogador será teletransportado")]
    public string nomeDaCenaDestino;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            AudioMenage.instanceSound.PlayPortalSound();
            
            
            //SceneManager.LoadScene(nomeDaCenaDestino);
        }
    }
}
