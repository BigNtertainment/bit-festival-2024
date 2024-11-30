using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;
using SerializableDictionary.Scripts;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    UnityEvent<ItemData> action;

    [SerializeField]
    bool playerInteractable = true;

    [SerializeField]
    float reachableDistance = 2.5f;

    [SerializeField]
    SerializableDictionary<ItemData, float> reachableDistanceWithItem
        = new SerializableDictionary<ItemData, float>();

    [SerializeField]
    private float outlineScale = 1.2f;

    [SerializeField]
    private Color reachableOutlineColor;

    [SerializeField]
    private Color unreachableOutlineColor;

    private Renderer interactableRenderer;
    private Transform interactableTransform;
    private Transform playerTransform;
    private MovementIntention playerMovement;
    private Tooled playerTool;

    void Start()
    {
        interactableRenderer = GetComponent<Renderer>();
        interactableTransform = GetComponent<Transform>();

        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerMovement = player.GetComponent<MovementIntention>();
        playerTool = player.GetComponent<Tooled>();
    }


    void OnMouseOver()
    {
        // Set the scale on the shader material
        if (playerInteractable)
        {
            interactableRenderer.materials[1].SetFloat("_Scale", outlineScale);
        }
    }


    void OnMouseExit()
    {
        // Set the scale to zero to disable the outline
        if (playerInteractable)
        {
            interactableRenderer.materials[1].SetFloat("_Scale", 0.0f);
        }
    }

    void OnMouseDown()
    {
        if (!playerInteractable)
        {
            return;
        }

        if (IsReachable(playerTransform, playerTool.toolHeld))
        {
            Interact(playerTransform, playerTool.toolHeld);
        }
        else
        {
            playerMovement.SetDestination(
                interactableTransform.position
                    + (playerTransform.position - interactableTransform.position).normalized
                    * reachableDistance * 0.5f,
                this,
                playerTool.toolHeld
            );
        }
    }

    void Update()
    {
        if (playerInteractable)
        {
            Color outlineColor = IsReachable(playerTransform, playerTool.toolHeld)
                ? reachableOutlineColor : unreachableOutlineColor;
            interactableRenderer.materials[1].SetColor("_OutlineColor", outlineColor);
        }
    }

    public void Interact(Transform source, ItemData tool)
    {
        if (IsReachable(source, tool))
        {
            action.Invoke(tool);
        }
    }

    bool IsReachable(Transform byTransform, ItemData tool)
    {
        float distance = Vector3.Distance(
            interactableTransform.position,
            byTransform.position
        );

        if (tool && reachableDistanceWithItem.ContainsKey(tool))
        {
            return distance <= reachableDistanceWithItem.Get(tool);
        }
        else
        {
            return distance <= reachableDistance;
        }
    }
}
