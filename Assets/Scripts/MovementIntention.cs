using UnityEngine;
using UnityEngine.AI;

/// A wrapper for the NavMeshAgent component
public class MovementIntention : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private Vector3 targetPosition;
    private Interactable targetInteractable = null;
    private ItemData targetTool = null;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the NavMeshAgent finished walking to its target, perform the interaction
        if (!navMeshAgent.pathPending
            && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (targetInteractable)
            {
                targetInteractable.Interact(GetComponent<Transform>(), targetTool);

                targetInteractable = null;
                targetTool = null;
            }

            animator.SetBool("walking", false);
        }
        else
        {
            animator.SetBool("walking", true);
        }
    }

    public void SetDestination(Vector3 targetPos)
    {
        SetDestination(targetPos, null, null);
    }

    public void SetDestination(Vector3 targetPos, Interactable targetInteractable)
    {
        SetDestination(targetPos, targetInteractable, null);
    }

    public void SetDestination(Vector3 targetPos, Interactable targetInteractable, ItemData targetTool)
    {
        targetPosition = targetPos;
        this.targetInteractable = targetInteractable;
        this.targetTool = targetTool;

        navMeshAgent.SetDestination(targetPos);
    }
}
