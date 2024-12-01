using System;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int capacity;

    [SerializeField] private List<ItemData> items = new();

    private Tooled tooled;

    public event Action InventoryUpdated;

    public int? SelectedSlot { get; private set; }

    void Start()
    {
        tooled = GetComponent<Tooled>();
    }

    public bool AtCapacity()
    {
        return items.Count >= capacity;
    }

    public int GetCapacity()
    {
        return capacity;
    }

    public List<ItemData> GetItems()
    {
        return items;
    }

    public void AddItem(ItemData item)
    {
        if (AtCapacity())
        {
            Debug.LogError("Can't add items to inventory at its capacity!");
            return;
        }

        items.Add(item);
        InventoryUpdated?.Invoke();
    }

    public void ToggleSelectedSlot(int? slot)
    {
        if (slot >= items.Count)
        {
            return;
        }

        var isSlotAlreadySelected = slot == SelectedSlot;
        SelectedSlot = isSlotAlreadySelected ? null : slot;
        InventoryUpdated?.Invoke();

        if (SelectedSlot is { } selectedSlot)
            tooled.toolHeld = items[selectedSlot];
        else
            tooled.toolHeld = null;
    }

    [CanBeNull]
    public ItemData GetSelectedItem()
    {
        if (SelectedSlot is not { } selectedSlotIndex)
        {
            return null;
        }

        return items[selectedSlotIndex];
    }
}