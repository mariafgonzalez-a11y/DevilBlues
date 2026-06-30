using UnityEngine;
using TMPro; // Remova esta linha se não usar TextMeshPro

// Adicione este script ao GameObject de cada chave no cenário.
// Configure o keyId no Inspector (ex: "key_red", "key_blue").
public class KeyItem : MonoBehaviour
{
    [Header("Identificação")]
    [Tooltip("ID único desta chave. Deve ser igual ao keyId configurado na Porta correspondente.")]
    public string keyId = "key_1";

    [Header("Interação")]
    [Tooltip("Distância máxima para o prompt de interação aparecer.")]
    public float interactionRadius = 2.5f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("UI (opcional)")]
    [Tooltip("Texto de prompt, ex: '[E] Pegar Chave Vermelha'. Deixe vazio para não usar.")]
    public GameObject interactionPromptUI;

    private Transform _player;
    private bool _playerNearby;

    void Start()
    {
        // Busca o jogador pela tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
        else
            Debug.LogWarning($"[KeyItem:{keyId}] Nenhum GameObject com tag 'Player' encontrado.");

        if (interactionPromptUI != null)
            interactionPromptUI.SetActive(false);
    }

    void Update()
    {
        if (_player == null) return;

        float distance = Vector3.Distance(transform.position, _player.position);
        _playerNearby = distance <= interactionRadius;

        // Exibe/oculta o prompt
        if (interactionPromptUI != null)
            interactionPromptUI.SetActive(_playerNearby);

        // Coleta a chave ao pressionar a tecla
        if (_playerNearby && Input.GetKeyDown(interactionKey))
            Collect();
    }

    void Collect()
    {
        KeyInventory.Instance.CollectKey(keyId);
        if (interactionPromptUI != null)
            interactionPromptUI.SetActive(false);
        Destroy(gameObject);
    }

    // Desenha o raio de interação no Editor (Gizmos)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
