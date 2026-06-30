using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Coloque este script no painel DeathScreen.
/// Ele faz o fade do overlay escuro e do texto ao ser ativado.
/// </summary>
public class DeathScreenFade : MonoBehaviour
{
    [Header("Referências")]
    public Image overlay;          // Image com cor preta semitransparente
    public TextMeshProUGUI deathText;

    [Header("Configurações")]
    public float fadeDuration = 1f;
    public string message = "Você morreu...";
    [Range(0f, 1f)]
    public float overlayMaxAlpha = 0.7f;

    private float timer = 0f;

    void OnEnable()
    {
        timer = 0f;

        // Começa tudo invisível
        if (overlay != null)
        {
            var c = overlay.color;
            c.a = 0f;
            overlay.color = c;
        }

        if (deathText != null)
        {
            deathText.text = message;
            var c = deathText.color;
            c.a = 0f;
            deathText.color = c;
        }
    }

    void Update()
    {
        if (timer >= fadeDuration) return;

        timer += Time.unscaledDeltaTime; // unscaled para funcionar mesmo com Time.timeScale = 0
        float t = Mathf.Clamp01(timer / fadeDuration);

        if (overlay != null)
        {
            var c = overlay.color;
            c.a = Mathf.Lerp(0f, overlayMaxAlpha, t);
            overlay.color = c;
        }

        if (deathText != null)
        {
            var c = deathText.color;
            c.a = Mathf.Lerp(0f, 1f, t);
            deathText.color = c;
        }
    }
}
