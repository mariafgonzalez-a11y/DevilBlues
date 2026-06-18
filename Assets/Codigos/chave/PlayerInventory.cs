using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // List to store all collected keys
    private HashSet<KeyType> collectedKeys = new HashSet<KeyType>();

    public void AddKey(KeyType key)
    {
        collectedKeys.Add(key);
        Debug.Log($"Picked up: {key}");
    }

    public bool HasKey(KeyType key)
    {
        return collectedKeys.Contains(key);
    }

    public void RemoveKey(KeyType key)
    {
        collectedKeys.Remove(key);
    }
}
