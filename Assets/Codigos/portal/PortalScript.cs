using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PortalScript : MonoBehaviour
{
    [Tooltip("Nome ou índice da cena para onde o jogador será teletransportado")]
    public string nomeDaCenaDestino;

    [Header("Tela de transição")]
    public float duracaoTela = 3f;       // Segundos que a tela fica visível
    public float velocidadeFade = 1.5f;  // Velocidade do fade in

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(Transicao());
    }

    IEnumerator Transicao()
    {
        // Cria o Canvas da tela preta dinamicamente
        GameObject canvasGO = new GameObject("TelaTransicao");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(canvasGO);

        // Fundo preto
        GameObject fundoGO = new GameObject("Fundo");
        fundoGO.transform.SetParent(canvasGO.transform, false);
        Image fundo = fundoGO.AddComponent<Image>();
        fundo.color = new Color(0f, 0f, 0f, 0f);
        RectTransform rt = fundo.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Texto
        GameObject textoGO = new GameObject("Texto");
        textoGO.transform.SetParent(canvasGO.transform, false);
        TextMeshProUGUI texto = textoGO.AddComponent<TextMeshProUGUI>();
        texto.text = "Fase em andamento...";
        texto.fontSize = 80;
        texto.alignment = TextAlignmentOptions.Center;
        texto.color = new Color(1f, 1f, 1f, 0f);
        RectTransform rtTexto = texto.GetComponent<RectTransform>();
        rtTexto.anchorMin = new Vector2(0f, 0.4f);
        rtTexto.anchorMax = new Vector2(1f, 0.6f);
        rtTexto.offsetMin = Vector2.zero;
        rtTexto.offsetMax = Vector2.zero;

        // Fade in
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * velocidadeFade;
            float alpha = Mathf.Clamp01(timer);
            fundo.color  = new Color(0f, 0f, 0f, alpha);
            texto.color  = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        // Carrega a cena
        SceneManager.LoadScene(nomeDaCenaDestino);

        // Aguarda e destrói o canvas
        yield return new WaitForSeconds(duracaoTela);
        Destroy(canvasGO);
    }
}

