using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Executioner : MonoBehaviour
{
    private MovementIntention movement;
    private Transform executionerTransform;

    public enum State
    {
        Announcing,
        Eating,
        Hanging,
        FixingLever,
        Investing,
        LookingAround,
        ChasingPlayer
    }

    [SerializeField]
    public State? currentState = State.Announcing;

    [SerializeField]
    GameObject announcementTrigger;

    [SerializeField]
    GameObject bananaEatingTrigger;

    [SerializeField]
    GameObject bananaPeelPrefab;

    void Start()
    {
        movement = GetComponent<MovementIntention>();
        executionerTransform = GetComponent<Transform>();
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
        }
    }

    private State? prevState = null;

    void Update()
    {
        if (prevState != currentState)
        {
            // TODO: If in dialogue, wait with changing the state

            OnStateChange();
            prevState = currentState;
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

    public void HandleInteractions(ItemData item)
    {
        if (!item)
        {
            // TODO: Make it a sound and a dialogue
            Debug.Log("What do you want?");
            return;
        }

        if (item.itemName == "stone")
        {
            if (currentState == State.ChasingPlayer)
            {
                return;
            }

            if (currentState == State.FixingLever)
            {
                // TODO: Fall over and lose dagger
                return;
            }

            // TODO: Also add a short dialogue here
            // But think of a way to make it and not break the game
            Debug.Log("Now you've done it.");

            currentState = State.Hanging;
        }
    }
}
