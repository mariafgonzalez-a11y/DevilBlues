using System.Collections;
using UnityEngine;

// Adicione este script ao GameObject de cada porta.
// Configure o keyId para corresponder à chave que abre esta porta.
public class Door : MonoBehaviour
{
    [Header("Identificação")]
    [Tooltip("ID da chave que abre esta porta. Deve ser igual ao keyId da KeyItem correspondente.")]
    public string requiredKeyId = "key_1";

    [Header("Interação")]
    public float interactionRadius = 2.5f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("Animação de Abertura (Rotação)")]
    [Tooltip("Ângulo de rotação ao abrir (graus). Ex: 90 para abrir para o lado.")]
    public float openAngle = 90f;
    [Tooltip("Eixo de rotação local da porta.")]
    public Vector3 rotationAxis = Vector3.up;
    [Tooltip("Duração da animação em segundos.")]
    public float animationDuration = 0.6f;

    [Header("UI (opcional)")]
    public GameObject interactionPromptUI;
    public GameObject lockedPromptUI; // Ex: "[Bloqueado] Você precisa de uma chave"

    private Transform _player;
    private bool _isOpen = false;
    private bool _isAnimating = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
        else
            Debug.LogWarning($"[Door:{requiredKeyId}] Nenhum GameObject com tag 'Player' encontrado.");

        _closedRotation = transform.rotation;
        _openRotation = _closedRotation * Quaternion.AngleAxis(openAngle, rotationAxis);

        SetPrompts(false, false);
    }

    void Update()
    {
        if (_player == null || _isAnimating || _isOpen) return;

        float distance = Vector3.Distance(transform.position, _player.position);
        bool nearby = distance <= interactionRadius;

        bool hasKey = KeyInventory.Instance.HasKey(requiredKeyId);

        // Mostra o prompt correto
        if (interactionPromptUI != null)
            interactionPromptUI.SetActive(nearby && hasKey);
        if (lockedPromptUI != null)
            lockedPromptUI.SetActive(nearby && !hasKey);

        // Tenta abrir
        if (nearby && Input.GetKeyDown(interactionKey))
        {
            if (hasKey)
                StartCoroutine(OpenDoor());
            else
                Debug.Log($"[Door] Porta bloqueada. Necessário: {requiredKeyId}");
        }
    }

    IEnumerator OpenDoor()
    {
        _isAnimating = true;
        SetPrompts(false, false);

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animationDuration);
            transform.rotation = Quaternion.Lerp(_closedRotation, _openRotation, t);
            yield return null;
        }

        transform.rotation = _openRotation;
        _isOpen = true;
        _isAnimating = false;

        Debug.Log($"[Door] Porta aberta com a chave: {requiredKeyId}");
    }

    void SetPrompts(bool showUnlock, bool showLocked)
    {
        if (interactionPromptUI != null) interactionPromptUI.SetActive(showUnlock);
        if (lockedPromptUI != null) lockedPromptUI.SetActive(showLocked);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
