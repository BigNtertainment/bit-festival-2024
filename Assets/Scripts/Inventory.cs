using System;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int capacity;

    [SerializeField] private List<ItemData> items = new();

    public event Action InventoryUpdated;

    public int? SelectedSlot { get; private set; }

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
    }
}