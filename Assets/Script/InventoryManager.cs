using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    public Image[] slotIcons; // Tarik UI Image dari Slot 1, Slot 2, dst
    public Text[] slotAmountTexts; // (Opsional) Tarik komponen Text angka jumlah di setiap slot ke sini kalau ada

    [Header("Detail Panel UI")]
    public GameObject detailPanel;
    public Text detailNameText;
    public Text detailDescText;
    public Image detailIcon;

    // Class pembungkus untuk menyimpan Item dan Jumlahnya (Stack)
    [System.Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int stackSize;

        public InventoryItem(ItemData item)
        {
            itemData = item;
            stackSize = 1;
        }
    }

    private List<InventoryItem> inventory = new List<InventoryItem>();

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
        // Tekan tombol TAB untuk buka/tutup Inventory (sekaligus nutup detail)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isInventoryOpen = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(isInventoryOpen);

            if (!isInventoryOpen && detailPanel != null)
            {
                detailPanel.SetActive(false);
            }
        }
    }

    // Fungsi Tambah Item dengan Logika Stacking
    public void AddItem(ItemData newItem)
    {
        // 1. Cek apakah item yang sama sudah ada di inventory
        InventoryItem existingItem = inventory.Find(i => i.itemData == newItem);

        if (existingItem != null)
        {
            // Kalau sudah ada, tambahkan jumlah stack-nya!
            existingItem.stackSize++;
            Debug.Log("Stack bertambah untuk: " + newItem.itemName + " (Total: " + existingItem.stackSize + ")");
        }
        else
        {
            // Kalau belum ada dan slot masih muat, buat slot baru
            if (inventory.Count < slotIcons.Length)
            {
                inventory.Add(new InventoryItem(newItem));
                Debug.Log("Item baru masuk inventory: " + newItem.itemName);
            }
            else
            {
                Debug.Log("Inventory Penuh!");
                return;
            }
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            if (i < inventory.Count)
            {
                slotIcons[i].sprite = inventory[i].itemData.icon;
                slotIcons[i].color = Color.white; // Munculkan icon

                // Kalau kamu bikin Text angka jumlah stack di slot, aktifkan baris bawah ini:
                /*
                if (slotAmountTexts[i] != null)
                {
                    slotAmountTexts[i].text = inventory[i].stackSize > 1 ? inventory[i].stackSize.ToString() : "";
                }
                */
            }
            else
            {
                slotIcons[i].sprite = null;
                slotIcons[i].color = new Color(1, 1, 1, 0); // Sembunyikan icon kalau kosong

                /*
                if (slotAmountTexts[i] != null)
                {
                    slotAmountTexts[i].text = "";
                }
                */
            }
        }
    }

    // Dipanggil saat slot di-klik via script InventorySlot
    public void ShowDetail(int slotIndex)
    {
        if (slotIndex < inventory.Count)
        {
            detailPanel.SetActive(true);
            ItemData clickedItem = inventory[slotIndex].itemData;
            int count = inventory[slotIndex].stackSize;

            detailNameText.text = clickedItem.itemName + (count > 1 ? " (x" + count + ")" : "");
            detailDescText.text = clickedItem.description;
            detailIcon.sprite = clickedItem.icon;
        }
    }
}