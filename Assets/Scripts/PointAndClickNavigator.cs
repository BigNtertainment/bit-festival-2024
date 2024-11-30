using UnityEngine;
using UnityEngine.AI;

public class PointAndClickNavigator : MonoBehaviour
{
    public Camera mainCamera;

    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var inputMousePosition = Input.mousePosition;
        MovePlayerToMouse(inputMousePosition);
    }

    /**
     * Moves player to the mouse position if the casted ray hits anything.
     */
    private void MovePlayerToMouse(Vector3 mouseInputPosition)
    {
        if (GetMouseWorldPosition(mouseInputPosition) is not { } mouseWorldPosition)
        {
            return;
        }

        _navMeshAgent.SetDestination(mouseWorldPosition);
    }

    /**
     * Returns the position of the user mouse in the 3D world.
     */
    private Vector3? GetMouseWorldPosition(Vector3 mouseInputPosition)
    {
        var ray = mainCamera.ScreenPointToRay(mouseInputPosition);
        if (!Physics.Raycast(ray, out var raycastHitInfo))
        {
            return null;
        }

        return raycastHitInfo.point;
    }
}