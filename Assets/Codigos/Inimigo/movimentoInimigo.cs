using UnityEngine;

public class InimigoMovimento : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 5f; // Velocidade do inimigo

    private Transform jogador; // Referência à posição do jogador

    void Start()
    {
        // Procura automaticamente o objeto com a tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObj != null)
        {
            jogador = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado! Verifique se a tag 'Player' está atribuída.");
        }
    }

    void Update()
    {
        // Se o jogador existir na cena, o inimigo se move em direção a ele
        if (jogador != null)
        {
            // Move o inimigo suavemente em direção à posição do jogador
            transform.position = Vector2.MoveTowards(transform.position, jogador.position, velocidade * Time.deltaTime);
            
            // Alternativa para jogos 3D usando física (Rigidbody):
            // Vector3 direcao = (jogador.position - transform.position).normalized;
            // transform.position += direcao * velocidade * Time.deltaTime;
        }
    }
}
