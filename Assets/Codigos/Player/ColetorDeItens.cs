using UnityEngine;

public class ColetorDeItens : MonoBehaviour
{
    // Variável para guardar a pontuação ou quantidade de itens
    public int itensColetados = 0;

    // Essa função é chamada automaticamente quando o objeto entra em um "Trigger"
    private void OnTriggerEnter(Collider outroObjeto)
    {
        // Verifica se o objeto com o qual colidimos tem a Tag "Coletavel"
        if (outroObjeto.CompareTag("Coletavel"))
        {
            // Aumenta a quantidade de itens coletados
            itensColetados++;
            
            // Mostra no console para você saber que funcionou
            Debug.Log("Item coletado! Total: " + itensColetados);

            // Destrói o item da cena
            Destroy(outroObjeto.gameObject);
            
            // Opcional: Aqui você pode tocar um som ou soltar uma partícula!
        }
    }
}