using Unity.VisualScripting;
using UnityEngine;

public class LeverMechanism : MonoBehaviour
{
    public bool broken = false;

    [SerializeField]
    Trapdoor trapdoor;

    [SerializeField]
    Executioner executioner;

    public void Interact(ItemData tool)
    {
        if (!tool)
        {
            if (!broken)
            {
                trapdoor.Open();
            }
            else
            {
                if (executioner.currentState == Executioner.State.FixingLever)
                {
                    executioner.FixLever(this);

                    Debug.Log("Gotta fix the lever!");
                }

                if (executioner.currentState == Executioner.State.Hanging)
                {
                    executioner.currentState = Executioner.State.FixingLever;
                }
            }

            return;
        }

        if (tool.itemName != "pebble")
        {
            return;
        }

        broken = true;
    }
}
