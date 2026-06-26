using System.Collections.Generic;
using UnityEngine;

// Adicione este script a um GameObject vazio chamado "GameManager" na cena.
// Ele persiste durante toda a sessão e guarda as chaves coletadas.
public class KeyInventory : MonoBehaviour
{
    public static KeyInventory Instance { get; private set; }

    private HashSet<string> _collectedKeys = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>Registra uma chave coletada pelo seu ID único.</summary>
    public void CollectKey(string keyId)
    {
        if (_collectedKeys.Add(keyId))
            Debug.Log($"[KeyInventory] Chave coletada: {keyId}");
    }

    /// <summary>Verifica se o jogador possui uma chave específica.</summary>
    public bool HasKey(string keyId) => _collectedKeys.Contains(keyId);
}
