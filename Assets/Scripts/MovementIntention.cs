using UnityEngine;
using UnityEngine.AI;

/// A wrapper for the NavMeshAgent component
public class MovementIntention : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private Vector3 targetPosition;
    private Interactable targetInteractable = null;
    private ItemData targetTool = null;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();        
    }

    void Update()
    {
        // If the NavMeshAgent finished walking to its target, perform the interaction
        if (targetInteractable && !navMeshAgent.pathPending
            && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
            targetInteractable.Interact(targetTool);

            targetInteractable = null;
            targetTool = null;
        }
    }

    public void SetDestination(Vector3 targetPos) {
        SetDestination(targetPos, null, null);
    }

    public void SetDestination(Vector3 targetPos, Interactable targetInteractable) {
        SetDestination(targetPos, targetInteractable, null);
    }

    public void SetDestination(Vector3 targetPos, Interactable targetInteractable, ItemData targetTool) {
        targetPosition = targetPos;
        this.targetInteractable = targetInteractable;
        this.targetTool = targetTool;

        navMeshAgent.SetDestination(targetPos);
    }
}
