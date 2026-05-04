using UnityEngine;

public class Porta : MonoBehaviour
{
    // Quando a área de detecção (Trigger) for ativada...
    private void OnTriggerEnter(Collider outroObjeto)
    {
        // Verifica se quem chegou perto foi o Player
        if (outroObjeto.CompareTag("Player"))
        {
            // Procura o script de pontuação no Player
            Pontuacao pontuacaoDoPlayer = outroObjeto.GetComponent<Pontuacao>();
            
            if (pontuacaoDoPlayer != null)
            {
                // A porta "pergunta" se o Player tem uma chave para gastar
                if (pontuacaoDoPlayer.UsarChave() == true)
                {
                    Debug.Log("A porta abriu!");
                    
                    // Destrói a porta para liberar o caminho
                    Destroy(gameObject); 
                }
                else
                {
                    Debug.Log("A porta está trancada. Encontre a chave!");
                }
            }
        }
    }
}