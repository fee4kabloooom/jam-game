using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Run = 1,
        Attack = 2,
        Death = 3
    }

    [Header("General")]
    public GameObject sprite;

    NavMeshAgent agent;
    Animator anim;
    State currentState;
    bool canChangeState;
    bool turn;
    float changedEnd;

    //default methods
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!canChangeState) if (Time.time > changedEnd) canChangeState = true;
        ChangeAnimation();
    }

    //public methods
    public void ChangeState(State state, float end)
    {
        currentState = state;
        changedEnd = Time.time + end;
        canChangeState = false;
    }

    //private methods
    private void ChangeAnimation()
    {
        Vector2 deltaPos = agent.velocity;
        if (deltaPos != Vector2.zero)
        {
            if (deltaPos.x > 0) sprite.transform.localScale = new Vector3(-1, 1, 1);
            else if (deltaPos.x < 0) sprite.transform.localScale = new Vector3(1, 1, 1);

            if (deltaPos.y > 0) turn = true;
            else if (deltaPos.y < 0) turn = false;
            if (canChangeState) ChangeState(State.Run, 0);
        }
        else if (canChangeState) ChangeState(State.Idle, 0);

        anim.SetInteger("state", (int)currentState);
        anim.SetBool("turn", turn);
    }
}
