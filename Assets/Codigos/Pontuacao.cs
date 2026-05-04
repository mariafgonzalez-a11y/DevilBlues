using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{
    public int itensColetados = 0;
    public TextMeshProUGUI textoNaTela; 

    // Função que ganha chave (Você já tem essa)
    public void AdicionarPonto()
    {
        itensColetados++; 
        textoNaTela.text = "Chaves: " + itensColetados; 
    }

    // NOVA FUNÇÃO: Tenta gastar uma chave para abrir algo
    public bool UsarChave()
    {
        // Verifica se o jogador tem pelo menos 1 chave
        if (itensColetados > 0)
        {
            itensColetados--; // Gasta 1 chave (diminui o número)
            textoNaTela.text = "Chaves: " + itensColetados; // Atualiza a tela
            return true; // Retorna "Verdadeiro" (Sucesso, pode abrir a porta!)
        }
        
        // Se chegar aqui, é porque ele tem 0 chaves
        return false; // Retorna "Falso" (A porta continua trancada)
    }
}