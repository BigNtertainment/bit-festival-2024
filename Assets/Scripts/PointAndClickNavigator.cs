using UnityEngine;
using UnityEngine.EventSystems;

public class PointAndClickNavigator : MonoBehaviour
{
    public Camera mainCamera;

    private MovementIntention _movementIntention;

    private void Start()
    {
        _movementIntention = GetComponent<MovementIntention>();
    }

    private void Update()
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (GetMouseWorldPosition(mouseInputPosition) is not { } mouseWorldPosition)
        {
            return;
        }

        _movementIntention.SetDestination(mouseWorldPosition);
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

        const int FLOOR_LAYER = 6;

        if (raycastHitInfo.collider.gameObject.layer != FLOOR_LAYER)
        {
            return null;
        }

        return raycastHitInfo.point;
    }
}