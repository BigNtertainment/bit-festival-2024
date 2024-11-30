using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    ItemData item;

    [SerializeField]
    bool oneTimeUse = true;

    [SerializeField]
    float reachableDistance = 2.5f;

    [SerializeField]
    private float outlineScale = 1.2f;

    [SerializeField]
    private Color reachableOutlineColor;
    
    [SerializeField]
    private Color unreachableOutlineColor;

    private Renderer pickableRenderer;
    private Transform pickableTransform;
    private Transform playerTransform;
    private Inventory playerInventory;

    private bool reachable = false;

    void Start() {
        pickableRenderer = GetComponent<Renderer>();
        pickableTransform = GetComponent<Transform>();

        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerInventory = player.GetComponent<Inventory>();
    }

    void OnMouseOver() {
        // Set the scale on the shader material
        pickableRenderer.materials[1].SetFloat("_Scale", outlineScale);
    }

    void OnMouseExit() {
        // Set the scale to zero to disable the outline
        pickableRenderer.materials[1].SetFloat("_Scale", 0.0f);
    }

    void OnMouseDown() {
        if(reachable && !playerInventory.AtCapacity()) {
            playerInventory.AddItem(item);

            if(oneTimeUse) {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(
            pickableTransform.position,
            playerTransform.position
        );

        reachable = distanceToPlayer <= reachableDistance;

        Color outlineColor = reachable ? reachableOutlineColor : unreachableOutlineColor;
        pickableRenderer.materials[1].SetColor("_OutlineColor", outlineColor);
    }
}
