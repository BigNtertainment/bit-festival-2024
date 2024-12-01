using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trapdoor : MonoBehaviour
{
    bool open = false;

    public List<GameObject> colliders = new List<GameObject>();
    private NavMeshObstacle navMeshObstacle;

    void Start()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
    }

    public void Open()
    {
        open = true;
        navMeshObstacle.enabled = true;

        foreach (GameObject obj in colliders)
        {
            Debug.Log(obj.name + " fell through the hole :0");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        colliders.Add(collider.gameObject);
    }

    void OnTriggerExit(Collider collider)
    {
        colliders.Remove(collider.gameObject);
    }
}
