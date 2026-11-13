using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton biar gampang dipanggil

    [Header("Inventory UI")]
    public GameObject inventoryPanel; // Panel keseluruhan inventory
    public Image[] slotIcons; // Tarik UI Image dari Slot 1, Slot 2, dst ke sini

    [Header("Detail Panel UI")]
    public GameObject detailPanel;
    public Text detailNameText;
    public Text detailDescText;
    public Image detailIcon;

    private List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
        detailPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        // Tekan tombol TAB untuk buka/tutup Inventory
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void AddItem(ItemData newItem)
    {
        if (items.Count < slotIcons.Length)
        {
            items.Add(newItem);
            UpdateUI();
        }
        else
        {
            Debug.Log("Inventory Penuh!");
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            if (i < items.Count)
            {
                slotIcons[i].sprite = items[i].icon;
                slotIcons[i].color = Color.white; // Munculkan icon
            }
            else
            {
                slotIcons[i].sprite = null;
                slotIcons[i].color = new Color(1, 1, 1, 0); // Sembunyikan icon kalau kosong
            }
        }
    }

    // Dipanggil saat slot di-klik
    public void ShowDetail(int slotIndex)
    {
        if (slotIndex < items.Count)
        {
            detailPanel.SetActive(true);
            ItemData clickedItem = items[slotIndex];
            detailNameText.text = clickedItem.itemName;
            detailDescText.text = clickedItem.description;
            detailIcon.sprite = clickedItem.icon;
        }
    }
}