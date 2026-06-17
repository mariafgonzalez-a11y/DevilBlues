using System.Collections;
using UnityEngine;
using TMPro;

public class IntroFade : MonoBehaviour
{
    [Header("Referências da Intro")]
    public CanvasGroup fundoPretoGroup;
    public CanvasGroup textoGroup;
    public TextMeshProUGUI introText;

    [Header("Tutorial")]
    public GameObject tutorialControls;

    [Header("Texto sem os pontinhos")]
    [TextArea]
    public string mensagemBase = "Chuck acorda na prisão das almas";

    [Header("Tempos da mensagem")]
    public float tempoFadeInTexto = 1.5f;
    public float tempoMensagemNaTela = 5f;
    public float tempoFadeOutTexto = 1.5f;

    [Header("Fade out da tela preta")]
    public float tempoFadeOutFundo = 2f;

    [Header("Tempo dos pontinhos")]
    public float tempoUmPonto = 1f;
    public float tempoDoisPontos = 3f;
    public float tempoTresPontos = 2f;

    private bool animarPontinhos = true;

    void Start()
    {
        // Intro começa ativa
        fundoPretoGroup.alpha = 1f;
        textoGroup.alpha = 0f;

        fundoPretoGroup.gameObject.SetActive(true);
        textoGroup.gameObject.SetActive(true);

        // Tutorial começa desligado
        if (tutorialControls != null)
        {
            tutorialControls.SetActive(false);
        }

        StartCoroutine(RodarIntro());
        StartCoroutine(AnimarPontinhos());
    }

    IEnumerator RodarIntro()
    {
        // Texto aparece
        yield return Fade(textoGroup, 0f, 1f, tempoFadeInTexto);

        // Texto fica na tela
        yield return new WaitForSeconds(tempoMensagemNaTela);

        // Texto some
        yield return Fade(textoGroup, 1f, 0f, tempoFadeOutTexto);

        // Para os pontinhos
        animarPontinhos = false;

        // Tela preta some
        yield return Fade(fundoPretoGroup, 1f, 0f, tempoFadeOutFundo);

        // Agora que a intro acabou, ativa o tutorial
        if (tutorialControls != null)
        {
            tutorialControls.SetActive(true);
        }

        // Depois desativa a intro
        gameObject.SetActive(false);
    }

    IEnumerator AnimarPontinhos()
    {
        while (animarPontinhos)
        {
            introText.text = mensagemBase + ".";
            yield return new WaitForSeconds(tempoUmPonto);

            introText.text = mensagemBase + "..";
            yield return new WaitForSeconds(tempoDoisPontos);

            introText.text = mensagemBase + "...";
            yield return new WaitForSeconds(tempoTresPontos);
        }
    }

    IEnumerator Fade(CanvasGroup grupo, float inicio, float fim, float duracao)
    {
        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            grupo.alpha = Mathf.Lerp(inicio, fim, tempo / duracao);
            yield return null;
        }

        grupo.alpha = fim;
    }
}