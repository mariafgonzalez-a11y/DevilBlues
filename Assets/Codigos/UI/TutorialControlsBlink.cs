using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControlsBlink : MonoBehaviour
{
    [Header("Canvas Group do Tutorial")]
    public CanvasGroup tutorialGroup;

    [Header("Imagens da UI")]
    public Image wasdImage;
    public Image spaceImage;
    public Image mouseImage;
    public Image mousePressedImage;

    [Header("Tempos do Tutorial")]
    public float tempoFadeIn = 1f;
    public float tempoNaTela = 6f;
    public float tempoFadeOut = 1f;

    [Header("Cores do piscar")]
    public Color corNormal = new Color(0.55f, 0.55f, 0.55f, 1f);
    public Color corClara = new Color(1f, 1f, 1f, 1f);

    [Header("Velocidade do piscar")]
    public float velocidadePiscar = 1.2f;

    [Header("Mouse Pressed")]
    public float velocidadeMousePressed = 1.8f;
    public float escalaMousePressed = 1.08f;

    private Vector3 escalaOriginalMousePressed;
    private bool tutorialAtivo = false;

    void Start()
    {
        if (tutorialGroup != null)
        {
            tutorialGroup.alpha = 0f;
            tutorialGroup.interactable = false;
            tutorialGroup.blocksRaycasts = false;
        }

        if (mousePressedImage != null)
        {
            escalaOriginalMousePressed = mousePressedImage.rectTransform.localScale;
        }

        ForcarAlphaVisivel(wasdImage);
        ForcarAlphaVisivel(spaceImage);
        ForcarAlphaVisivel(mouseImage);
        ForcarAlphaVisivel(mousePressedImage);

        if (mouseImage != null)
        {
            mouseImage.color = Color.white;
        }

        StartCoroutine(RodarTutorial());
    }

    void Update()
    {
        if (!tutorialAtivo) return;

        PiscarImagem(wasdImage, velocidadePiscar);
        PiscarImagem(spaceImage, velocidadePiscar);

        // Mouse normal fica parado
        PiscarMousePressed();
    }

    IEnumerator RodarTutorial()
    {
        // Fade in do tutorial
        yield return FadeTutorial(0f, 1f, tempoFadeIn);

        tutorialAtivo = true;

        // Tempo que o tutorial fica aparecendo
        yield return new WaitForSeconds(tempoNaTela);

        tutorialAtivo = false;

        // Fade out do tutorial
        yield return FadeTutorial(1f, 0f, tempoFadeOut);

        // Some do jogo
        gameObject.SetActive(false);
    }

    IEnumerator FadeTutorial(float inicio, float fim, float duracao)
    {
        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;

            if (tutorialGroup != null)
            {
                tutorialGroup.alpha = Mathf.Lerp(inicio, fim, tempo / duracao);
            }

            yield return null;
        }

        if (tutorialGroup != null)
        {
            tutorialGroup.alpha = fim;
        }
    }

    void PiscarImagem(Image imagem, float velocidade)
    {
        if (imagem == null) return;

        float t = Mathf.PingPong(Time.time * velocidade, 1f);
        t = Mathf.SmoothStep(0f, 1f, t);

        Color corAtual = Color.Lerp(corNormal, corClara, t);

        // Não deixa a imagem transparente durante o piscar
        corAtual.a = 1f;

        imagem.color = corAtual;
    }

    void PiscarMousePressed()
    {
        if (mousePressedImage == null) return;

        float t = Mathf.PingPong(Time.time * velocidadeMousePressed, 1f);
        t = Mathf.SmoothStep(0f, 1f, t);

        Color corAtual = Color.Lerp(corNormal, corClara, t);

        // Não deixa transparente durante o piscar
        corAtual.a = 1f;

        mousePressedImage.color = corAtual;

        float escalaAtual = Mathf.Lerp(1f, escalaMousePressed, t);
        mousePressedImage.rectTransform.localScale = escalaOriginalMousePressed * escalaAtual;
    }

    void ForcarAlphaVisivel(Image imagem)
    {
        if (imagem == null) return;

        Color cor = imagem.color;
        cor.a = 1f;
        imagem.color = cor;
    }
}