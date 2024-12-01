using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trapdoor : MonoBehaviour
{
    bool open = false;

    public List<GameObject> colliders = new List<GameObject>();
    private NavMeshObstacle navMeshObstacle;
    
    public CutToBlackSceneRestarter cutToBlackSceneRestarter;

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
            if (obj.CompareTag("Player"))
            {
                OpenCutToBlackScreen();
            }

            Debug.Log($"{obj.name} fell through the trapdoor.");
        }
    }

    private void OpenCutToBlackScreen()
    {
        cutToBlackSceneRestarter.CutToBlackAndRestartScene();
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