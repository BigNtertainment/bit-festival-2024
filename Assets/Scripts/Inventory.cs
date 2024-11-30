using System;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int capacity;

    [SerializeField]
    List<ItemData> items = new List<ItemData>();

    public event Action InventoryUpdated;

    public bool AtCapacity() {
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

    public void AddItem(ItemData item) {
        if(AtCapacity()) {
            Debug.LogError("Can't add items to inventory at its capacity!");
            return;
        }

        items.Add(item);
        InventoryUpdated?.Invoke();
    }
}
