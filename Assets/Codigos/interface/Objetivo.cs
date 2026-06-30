using UnityEngine;
using TMPro;

public class Objetivo : MonoBehaviour
{
    public static Objetivo Instance;

    public TextMeshProUGUI texto; // arraste o ObjetivoText aqui

    public string[] objetivos = new string[]
    {
        "Encontre a guitarra",
        "Colete a chave vermelha",
        "Abra a porta",
        "Mate o inimigo",
        "Pegue a chave azul",
        "Abra a porta dos fundos",
    };

    private int atual = 0;

    void Awake() => Instance = this;
    void Start()  => texto.text = "▶ " + objetivos[atual];

    // Chame este método quando o objetivo atual for concluído
    public void Completar()
    {
        atual++;

        if (atual < objetivos.Length)
            texto.text = "▶ " + objetivos[atual];
        else
            texto.text = "✔ Todos os objetivos concluídos!";
    }
}
