using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    private void OnTriggerEnter(Collider outroObjeto)
    {
        // Verifica se quem encostou foi o Player
        if (outroObjeto.CompareTag("Player"))
        {
            // Procura o script de Pontuação dentro do Player
            Pontuacao pontuacaoDoPlayer = outroObjeto.GetComponent<Pontuacao>();
            
            // Se ele encontrou o script, avisa para adicionar 1 ponto
            if (pontuacaoDoPlayer != null)
            {
                pontuacaoDoPlayer.AdicionarPonto();
            }

            // Destrói o item da cena
            Destroy(gameObject);
        }
    }
}