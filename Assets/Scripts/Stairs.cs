using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField]
    private Executioner executioner;

    private GameObject player;

    private CutToBlackSceneRestarter sceneRestarter;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sceneRestarter = FindAnyObjectByType<CutToBlackSceneRestarter>();
    }

    public void GoDownTheStairs(ItemData item)
    {
        if (item)
        {
            return;
        }

        if (executioner.currentState != Executioner.State.StuckInAHole
            && executioner.currentState != Executioner.State.FallenDown)
        {
            // TODO: Make it a dialogue
            Debug.Log("Hey! Where do you think you're going??!");
            executioner.currentState = Executioner.State.ChasingPlayer;
            return;
        }

        if (GameObject.FindWithTag("BananaPeel"))
        {
            Debug.Log(player.name + " slipped");
            sceneRestarter.CutToBlackAndRestartScene();
            return;
        }

        Debug.Log("win!!!");
    }
}
