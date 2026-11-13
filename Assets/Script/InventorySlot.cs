using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public int slotIndex; // Isi 0 untuk slot pertama, 1 untuk slot kedua, dst di Inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        // Panggil fungsi tampilkan detail di Manager
        InventoryManager.Instance.ShowDetail(slotIndex);
    }
}