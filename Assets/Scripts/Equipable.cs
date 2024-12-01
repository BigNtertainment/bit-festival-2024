using UnityEngine;

public class Equipable : MonoBehaviour
{
    [SerializeField]
    ItemData item;

    [SerializeField]
    bool oneTimeUse = true;

    private Inventory playerInventory;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }

    public void PickUp(ItemData tool)
    {
        if (tool != null)
        {
            Debug.Log("AAA: " + tool);

            return;
        }

        if (!playerInventory.AtCapacity())
        {
            playerInventory.AddItem(item);

            if (oneTimeUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
