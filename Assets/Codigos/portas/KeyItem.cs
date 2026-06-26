using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [Header("Key Settings")]
    [SerializeField] private KeyType keyType;

    private void OnTriggerEnter(Collider other)
    {
        
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            
            inventory.AddKey(keyType);
            Destroy(gameObject);
        }
    }
}

