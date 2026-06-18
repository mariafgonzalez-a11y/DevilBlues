using UnityEngine;


public class InimigoController : MonoBehaviour
{
    [Header("Atributos")]
    public float velocidade = 3f;
    public float vidaMaxima = 100f;
    public float distanciaAtaque = 1.5f;
    public float danoAtaque = 10f;
    
    private Transform player;
    private float vidaAtual;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if (player == null) return;

        // 1. Movimentação e Perseguição
        float distanciaDoPlayer = Vector2.Distance(transform.position, player.position);

        if (distanciaDoPlayer > distanciaAtaque)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);
        }
        else
        {
            // 2. Ataque
            Atacar();
        }
    }

    void Atacar()
    {
        Debug.Log("Inimigo atacando o player!");
        
    }

    // 3. Tomar Dano
    public void TomarDano(float quantidade)
    {
        vidaAtual -= quantidade;
        Debug.Log("Vida do inimigo: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    // 4. Morte
    void Morrer()
    {
        Debug.Log("Inimigo derrotado!");
        Destroy(gameObject); 
    }



    void OnTriggerEnter2D(Collider2D collision)
{
    
    InimigoController inimigo = collision.GetComponent<InimigoController>();
    if (inimigo != null)
    {
        inimigo.TomarDano(20f); 
        Destroy(gameObject); 
    }
}

}
