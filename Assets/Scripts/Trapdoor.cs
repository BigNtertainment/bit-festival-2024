using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trapdoor : MonoBehaviour
{
    [SerializeField]
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

            if (obj.CompareTag("Executioner"))
            {
                Executioner executioner = obj.GetComponent<Executioner>();
                executioner.currentState = Executioner.State.StuckInAHole;
            }
        }
    }

    private void OpenCutToBlackScreen()
    {
        cutToBlackSceneRestarter.CutToBlackAndRestartScene();
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name + " entered");

        colliders.Add(collider.gameObject);
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log(collider.gameObject.name + " exited");

        colliders.Remove(collider.gameObject);
    }
}