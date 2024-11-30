using UnityEngine;

public class LeverMechanism : MonoBehaviour
{
    public void Interact(ItemData tool) {
        if(!tool || tool.itemName != "pebble") {
            return;
        }

        Debug.Log("Mechanism is broken now :(");
    }
}
