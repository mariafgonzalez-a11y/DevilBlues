using UnityEngine;

public class EnemyPatrolSimple : MonoBehaviour
{
    [Header("Pontos de patrulha")]
    public Transform[] pontosPatrulha;

    [Header("Player")]
    public Transform player;

    [Header("Movimento")]
    public float velocidadePatrulha = 2f;
    public float velocidadePerseguicao = 4f;
    public float distanciaParaTrocar = 0.5f;

    [Header("Detec��o")]
    public float distanciaParaPerseguir = 6f;
    public float distanciaParaDesistir = 10f;

    private int pontoAtual = 0;
    private bool perseguindo = false;

    void Start()
    {
        AudioMenage.instanceSound.soundenemy();
    }

    void Update()
    {
        if (player == null) return;

        float distanciaDoPlayer = Vector3.Distance(transform.position, player.position);

        if (distanciaDoPlayer <= distanciaParaPerseguir)
        {
            perseguindo = true;
        }

        if (distanciaDoPlayer >= distanciaParaDesistir)
        {
            perseguindo = false;
        }

        if (perseguindo)
        {
            PerseguirPlayer();
        }
        else
        {
            Patrulhar();
        }
    }

    void Patrulhar()
    {
        if (pontosPatrulha == null || pontosPatrulha.Length == 0) return;

        Transform alvo = pontosPatrulha[pontoAtual];

        Vector3 destino = alvo.position;
        destino.y = transform.position.y;

        MoverPara(destino, velocidadePatrulha);

        float distancia = Vector3.Distance(transform.position, destino);

        if (distancia <= distanciaParaTrocar)
        {
            pontoAtual++;

            if (pontoAtual >= pontosPatrulha.Length)
            {
                pontoAtual = 0;
            }
        }
    }

    void PerseguirPlayer()
    {
        Vector3 destino = player.position;
        destino.y = transform.position.y;

        MoverPara(destino, velocidadePerseguicao);
    }

    void MoverPara(Vector3 destino, float velocidade)
    {
        Vector3 direcao = destino - transform.position;
        direcao.y = 0f;

        if (direcao.magnitude > 0.05f)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, 8f * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );
    }
}