using System.Collections;
using UnityEngine;

// ─────────────────────────────────────────────────────────────────────────────
// DoubleDoor.cs
// Coloque este script num GameObject PAI vazio (ex: "DoubleDoor_Blue").
// As duas folhas da porta devem ser filhos deste objeto.
//
// Hierarquia esperada:
//   DoubleDoor_Blue          ← este script aqui
//     ├─ DoorLeft            ← folha esquerda (pivô na borda esquerda)
//     └─ DoorRight           ← folha direita  (pivô na borda direita)
//
// IMPORTANTE — Pivô das folhas:
//   O pivô (origem) de cada folha deve estar na BORDA (dobradiça), não no centro.
//   Se o seu modelo tem pivô no centro, corrija no Blender/Maya, ou use um
//   GameObject vazio como pivô pai (veja comentário em "Corrigindo o pivô" abaixo).
// ─────────────────────────────────────────────────────────────────────────────

public class DoubleDoor : MonoBehaviour
{
    [Header("Identificação")]
    [Tooltip("ID da chave que abre esta porta dupla.")]
    public string requiredKeyId = "key_blue";

    [Header("Folhas da Porta")]
    public Transform leftDoor;   // arraste DoorLeft aqui
    public Transform rightDoor;  // arraste DoorRight aqui

    [Header("Interação")]
    public float interactionRadius = 3f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("Animação")]
    [Tooltip("Ângulo que cada folha vai rotacionar ao abrir. Use valor positivo; o script inverte para cada lado.")]
    public float openAngle = 90f;
    [Tooltip("Duração da animação em segundos.")]
    public float animationDuration = 0.7f;

    [Header("Direção de abertura")]
    [Tooltip(
        "Para Fora  → a porta empurra na direção OPOSTA ao jogador (ex: portão).\n" +
        "Para Dentro → a porta abre na direção DO jogador.\n" +
        "Fixed      → sempre abre no mesmo sentido (use o sinal de openAngle para definir).")]
    public OpenDirection openDirection = OpenDirection.TowardsOutside;

    public enum OpenDirection { TowardsOutside, TowardsInside, Fixed }

    [Header("UI (opcional)")]
    public GameObject interactionPromptUI;
    public GameObject lockedPromptUI;

    // ── estado interno ──
    private Transform _player;
    private bool _isOpen;
    private bool _isAnimating;

    private Quaternion _leftClosed, _rightClosed;
    private Quaternion _leftOpen,   _rightOpen;

    // ─────────────────────────────────────────────────────────────────────────

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) _player = p.transform;
        else Debug.LogWarning("[DoubleDoor] Nenhum GameObject com tag 'Player' encontrado.");

        if (leftDoor == null || rightDoor == null)
        {
            Debug.LogError("[DoubleDoor] Atribua as folhas leftDoor e rightDoor no Inspector!");
            enabled = false;
            return;
        }

        _leftClosed  = leftDoor.rotation;
        _rightClosed = rightDoor.rotation;

        // As rotações abertas são calculadas de forma lazy em OpenDoor(),
        // pois dependem da posição do jogador (para/fora ou para/dentro).
        SetPrompts(false, false);
    }

    void Update()
    {
        if (_player == null || _isAnimating || _isOpen) return;

        float dist    = Vector3.Distance(transform.position, _player.position);
        bool  nearby  = dist <= interactionRadius;
        bool  hasKey  = KeyInventory.Instance.HasKey(requiredKeyId);

        if (interactionPromptUI != null) interactionPromptUI.SetActive(nearby && hasKey);
        if (lockedPromptUI      != null) lockedPromptUI.SetActive(nearby && !hasKey);

        if (nearby && Input.GetKeyDown(interactionKey))
        {
            if (hasKey)
                StartCoroutine(OpenDoor());
            else
                Debug.Log($"[DoubleDoor] Bloqueada. Necessário: {requiredKeyId}");
        }
    }

    IEnumerator OpenDoor()
    {
        _isAnimating = true;
        SetPrompts(false, false);

        // Calcula o sinal de rotação de acordo com a direção desejada
        float sign = ComputeSign();

        // Rotação final de cada folha em torno do eixo Y local
        _leftOpen  = _leftClosed  * Quaternion.AngleAxis( sign * openAngle, Vector3.up);
        _rightOpen = _rightClosed * Quaternion.AngleAxis(-sign * openAngle, Vector3.up);

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animationDuration);
            leftDoor.rotation  = Quaternion.Lerp(_leftClosed,  _leftOpen,  t);
            rightDoor.rotation = Quaternion.Lerp(_rightClosed, _rightOpen, t);
            yield return null;
        }

        leftDoor.rotation  = _leftOpen;
        rightDoor.rotation = _rightOpen;
        _isOpen      = true;
        _isAnimating = false;

        Debug.Log($"[DoubleDoor] Porta dupla aberta com a chave: {requiredKeyId}");
    }

    // ─── Calcula se a porta deve empurrar para o lado do jogador ou oposto ───
    float ComputeSign()
    {
        if (openDirection == OpenDirection.Fixed)
            return 1f;

        // Vetor do centro da porta até o jogador, projetado no eixo forward da porta
        Vector3 toPlayer = _player.position - transform.position;
        float   dot      = Vector3.Dot(transform.forward, toPlayer);

        // dot > 0  → jogador está na frente da porta
        // dot < 0  → jogador está atrás da porta
        bool playerInFront = dot >= 0f;

        return openDirection switch
        {
            OpenDirection.TowardsOutside => playerInFront ? -1f :  1f,  // empurra para longe
            OpenDirection.TowardsInside  => playerInFront ?  1f : -1f,  // puxa para perto
            _ => 1f
        };
    }

    void SetPrompts(bool unlock, bool locked)
    {
        if (interactionPromptUI != null) interactionPromptUI.SetActive(unlock);
        if (lockedPromptUI      != null) lockedPromptUI.SetActive(locked);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);

        // Mostra o forward da porta (útil para ajustar a direção)
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// CORRIGINDO O PIVÔ (caso seu modelo tenha pivô no centro)
// ─────────────────────────────────────────────────────────────────────────────
// Se o mesh da folha tem origem no centro, faça assim:
//
//   1. Crie um GameObject vazio chamado "DoorLeft_Pivot"
//   2. Posicione-o na borda/dobradiça da porta
//   3. Arraste o mesh da porta como FILHO desse pivot
//   4. Mova o mesh filho para que a dobradiça fique alinhada ao pivot pai
//   5. Use "DoorLeft_Pivot" como leftDoor no Inspector
//
// Resultado: a rotação acontecerá em torno da dobradiça corretamente.
// ─────────────────────────────────────────────────────────────────────────────
