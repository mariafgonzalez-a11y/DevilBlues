using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Header("Lock Settings")]
    [SerializeField] private KeyType requiredKey;
    [SerializeField] private bool consumeKeyOnUse = false;

    [Header("Animation Settings")]
    [SerializeField] private Vector3 openRotationOffset = new Vector3(0, 90, 0);
    [SerializeField] private float speed = 2f;

    private bool isOpened = false;
    private Quaternion targetRotation;

    private void Awake()
    {
        targetRotation = transform.rotation;
    }

    private void Update()

    {
        // Smoothly rotate the door to its open target angle
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;

        //Remover item do inventário após abrir porta
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            // Check if player holds the correct key type
            if (inventory.HasKey(requiredKey))
            {
                OpenDoor();

                if (consumeKeyOnUse == true)
                {
                    inventory.RemoveKey(requiredKey);
                }
            }
            else
            {
                Debug.Log($"Locked! You specifically need a {requiredKey}.");
            }
        }
    }

    private void OpenDoor()
    {
        isOpened = true;
        targetRotation = transform.rotation * Quaternion.Euler(openRotationOffset);
        Debug.Log("Door successfully unlocked and opened!");
    }
}
