using UnityEngine;
using System.Collections;
using System;

public class Executioner : MonoBehaviour
{
    private MovementIntention movement;
    private Transform executionerTransform;

    [Serializable]
    public enum State
    {
        Announcing,
        Eating,
        Hanging,
        FixingLever,
        FallenDown,
        Investigating,
        ChasingPlayer,
        StuckInAHole
    }

    [SerializeField] public State? currentState = State.Announcing;

    [SerializeField] GameObject announcementTrigger;

    [SerializeField] GameObject bananaEatingTrigger;

    [SerializeField] GameObject lever;

    [SerializeField] GameObject trapdoor;

    [SerializeField] GameObject bananaPeelPrefab;

    [SerializeField] GameObject executionerSword;

    GameObject player;
    DialogueTrigger dialogueTrigger;

    void Start()
    {
        movement = GetComponent<MovementIntention>();
        executionerTransform = GetComponent<Transform>();

        player = GameObject.FindWithTag("Player");

        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void OnStateChange()
    {
        switch (currentState)
        {
            case State.Announcing:
                movement.SetDestination(
                    announcementTrigger.GetComponent<Transform>().position,
                    announcementTrigger.GetComponent<Interactable>()
                );
                break;

            case State.Eating:
                movement.SetDestination(
                    bananaEatingTrigger.GetComponent<Transform>().position,
                    bananaEatingTrigger.GetComponent<Interactable>()
                );
                break;

            case State.Hanging:
                if (!trapdoor.GetComponent<Trapdoor>().colliders.Contains(player))
                {
                    // TODO: Make it a dialogue
                    Debug.Log("Where is she?");

                    currentState = State.Investigating;

                    break;
                }

                movement.SetDestination(
                    lever.GetComponent<Transform>().position,
                    lever.GetComponent<Interactable>()
                );

                break;

            case State.FixingLever:
                movement.SetDestination(
                    lever.GetComponent<Transform>().position,
                    lever.GetComponent<Interactable>()
                );
                break;

            case State.FallenDown:
                StopAllCoroutines();
                StartCoroutine(StandingUp());
                break;

            case State.Investigating:
                movement.SetDestination(
                    trapdoor.GetComponent<Transform>().position,
                    trapdoor.GetComponent<Interactable>()
                );
                break;

            case State.StuckInAHole:
                dialogueTrigger.TriggerDialogue(new Dialogues.DialogueLine
                {
                    CharacterName = "Executioner",
                    Text = "I got stuck! I should've watched my weight like Annie told me to.",
                    Answers = new Dialogues.DialogueAnswer[] {
                        new() {
                            Text = "Hmm..."
                        }
                    }
                });
                break;
        }
    }

    private State? prevState = null;

    void Update()
    {
        if (prevState != currentState)
        {
            // TODO: If in dialogue, wait with changing the state

            prevState = currentState;
            OnStateChange();
        }

        if (currentState == State.ChasingPlayer)
        {
            movement.SetDestination(
                player.GetComponent<Transform>().position,
                player.GetComponent<Interactable>()
            );
        }
    }

    public void EatBanana()
    {
        StartCoroutine(EatingBanana());
    }

    IEnumerator EatingBanana()
    {
        yield return new WaitForSeconds(5.0f);

        Instantiate(
            bananaPeelPrefab,
            new Vector3(executionerTransform.position.x, 0.0f, executionerTransform.position.z),
            Quaternion.identity
        );

        // Who doesn't like a little after-lunch hanging
        currentState = State.Hanging;
    }

    public void FixLever(LeverMechanism lever)
    {
        StartCoroutine(FixingLever(lever));
    }

    IEnumerator FixingLever(LeverMechanism lever)
    {
        yield return new WaitForSeconds(5.0f);

        lever.broken = false;

        // TODO: Add some dialogue here

        currentState = State.Hanging;
    }

    IEnumerator StandingUp()
    {
        yield return new WaitForSeconds(5.0f);

        currentState = State.FixingLever;

        // I don't know why but it doesnt work without this
        // and i dont have the time to actually fix that
        lever.GetComponent<LeverMechanism>().broken = false;
    }

    public void LookAroundForPlayer()
    {
        StartCoroutine(LookingAround());
    }

    IEnumerator LookingAround()
    {
        yield return new WaitForSeconds(5.0f);

        currentState = State.ChasingPlayer;
    }

    public void HandleInteractions(ItemData item)
    {
        if (!item)
        {
            // TODO: Make it a sound and a dialogue
            Debug.Log("What do you want?");
            return;
        }

        if (item.itemName == "pebble")
        {
            if (currentState == State.ChasingPlayer
                || currentState == State.FallenDown)
            {
                Debug.Log("nie bij leżącego");
                return;
            }

            if (currentState == State.FixingLever)
            {
                currentState = State.FallenDown;
                DropSwordIfEquipped();
                return;
            }

            if (currentState == State.Investigating)
            {
                currentState = State.ChasingPlayer;
                return;
            }

            // TODO: Also add a short dialogue here
            // But think of a way to make it and not break the game
            Debug.Log("Now you've done it.");

            currentState = State.Hanging;
        }
    }

    void DropSwordIfEquipped()
    {
        var executionerSwordParent = executionerSword.transform.parent;

        if (executionerSwordParent != gameObject.transform)
        {
            return;
        }

        DetachSword();
    }

    void DetachSword()
    {
        executionerSword.transform.parent = null;
        executionerSword.GetComponent<Rigidbody>().isKinematic = false;
    }
}