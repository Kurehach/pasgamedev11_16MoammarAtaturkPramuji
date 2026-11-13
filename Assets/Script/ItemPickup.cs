using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // Tarik file ScriptableObject item-nya ke sini

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}