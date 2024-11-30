using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    private Inventory _playerInventory;
    private readonly List<GameObject> _itemSlots = new();

    [SerializeField] public Transform itemSlotContainer;
    [SerializeField] public GameObject itemSlotPrefab;

    private void Start()
    {
        _playerInventory = GetPlayerInventory();
        _playerInventory.InventoryUpdated += OnInventoryUpdate;

        var playerInventoryCapacity = _playerInventory.GetCapacity();
        PrepareSlots(playerInventoryCapacity);
        OnInventoryUpdate();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (_playerInventory.SelectedSlot == null) return;

        _playerInventory.ToggleSelectedSlot(null);
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

            ItemData itemData = null;
            if (i < items.Count) itemData = items[i];

            UpdateItemSlotBackground(itemSlot, i == _playerInventory.SelectedSlot);
            UpdateItemSlotImage(itemSlot, itemData);
        }
    }

    private void UpdateItemSlotBackground(GameObject itemSlot, bool isSelected)
    {
        var backgroundColor = new Color(0.8f, 0.8f, 0.8f, 1);
        if (isSelected) backgroundColor = Color.white;

        var itemSlotBackground = itemSlot.GetComponent<Image>();
        itemSlotBackground.color = backgroundColor;
    }

    private void UpdateItemSlotImage(GameObject itemSlot, [CanBeNull] ItemData itemData)
    {
        var imageChild = itemSlot.GetComponentsInChildren<Image>()
            .First(imageComponent => imageComponent.gameObject != itemSlot);

        if (itemData is null)
        {
            imageChild.color = new Color(1, 0, 1, 0);
            imageChild.sprite = null;
            return;
        }

        imageChild.color = Color.white;
        imageChild.sprite = itemData.icon;
    }

    private void PrepareSlots(int slotCount)
    {
        for (var i = 0; i < slotCount; i++)
        {
            var itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);

            var itemSlotButton = itemSlot.GetComponent<Button>();
            var itemSlotIndex = i;
            itemSlotButton.onClick.AddListener(() => OnSlotClick(itemSlotIndex));

            _itemSlots.Add(itemSlot);
        }
    }

    private void OnSlotClick(int slotIndex)
    {
        _playerInventory.ToggleSelectedSlot(slotIndex);
    }
}