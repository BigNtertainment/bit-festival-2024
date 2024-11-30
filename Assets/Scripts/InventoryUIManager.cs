using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    private Inventory _playerInventory;
    private readonly List<GameObject> _itemSlots = new();

    [SerializeField] public Transform itemSlotContainer;
    [SerializeField] public GameObject itemSlotPrefab;

    public Sprite emptySprite;

    void Start()
    {
        _playerInventory = GetPlayerInventory();
        _playerInventory.InventoryUpdated += OnInventoryUpdate;

        var playerInventoryCapacity = _playerInventory.GetCapacity();
        PrepareSlots(playerInventoryCapacity);
        OnInventoryUpdate();
    }

    private Inventory GetPlayerInventory()
    {
        var player = GameObject.FindWithTag("Player");
        return player.GetComponent<Inventory>();
    }

    private void OnInventoryUpdate()
    {
        var items = _playerInventory.GetItems();

        for (var i = 0; i < _itemSlots.Count; i++)
        {
            var itemSlot = _itemSlots[i];
            var imageChild = itemSlot.GetComponentsInChildren<Image>()
                .First(imageComponent => imageComponent.gameObject != itemSlot);

            if (i >= items.Count)
            {
                imageChild.color = new Color(1, 0, 1, 0);
                imageChild.sprite = null;
                continue;
            }

            var item = items[i];
            imageChild.color = Color.white;
            imageChild.sprite = item.icon;
        }
    }

    private void PrepareSlots(int slotCount)
    {
        for (var i = 0; i < slotCount; i++)
        {
            var itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            _itemSlots.Add(itemSlot);
        }
    }
}