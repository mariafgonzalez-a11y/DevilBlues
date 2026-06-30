using System.Collections.Generic;
using UnityEngine;


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

            Objetivo.Instance.Completar();
    }

    /// <summary>Verifica se o jogador possui uma chave específica.</summary>
    public bool HasKey(string keyId) => _collectedKeys.Contains(keyId);
}
