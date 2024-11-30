using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    UnityEvent action;

    [SerializeField]
    float reachableDistance = 2.5f;

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

    private bool reachable = false;

    void Start() {
        interactableRenderer = GetComponent<Renderer>();
        interactableTransform = GetComponent<Transform>();

        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerMovement = player.GetComponent<MovementIntention>();
    }

    void OnMouseOver() {
        // Set the scale on the shader material
        interactableRenderer.materials[1].SetFloat("_Scale", outlineScale);
    }

    void OnMouseExit() {
        // Set the scale to zero to disable the outline
        interactableRenderer.materials[1].SetFloat("_Scale", 0.0f);
    }

    void OnMouseDown() {
        if(reachable) {
            Interact();
        } else {
            playerMovement.SetDestination(
                interactableTransform.position
                    + (playerTransform.position - interactableTransform.position).normalized
                    * reachableDistance * 0.5f,
                this
            );
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(
            interactableTransform.position,
            playerTransform.position
        );

        reachable = distanceToPlayer <= reachableDistance;

        Color outlineColor = reachable ? reachableOutlineColor : unreachableOutlineColor;
        interactableRenderer.materials[1].SetColor("_OutlineColor", outlineColor);
    }

    public void Interact() {
        if(reachable) {
            action.Invoke();
        }
    }
}
