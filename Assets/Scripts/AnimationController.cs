using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("General")]
    public GameObject sprite;
    public GameObject slash;

    private Animator anim;
    private State currentState;
    private bool turn;
    private bool canChangeState = true;
    private float changedEnd;

    public enum State
    {
        Idle = 0,
        Run = 1,
        Attack = 2,
        Death = 3
    }

    //default methods
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        ChangeAnimation();
        if (Time.time >= changedEnd) canChangeState = true;
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
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        float mouseDifferenceX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;

        if (mouseDifferenceX > 0)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
            slash.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }
        else if (mouseDifferenceX < 0)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
            slash.transform.localScale = new Vector3(-1.5f, -1.5f, 1);
        }

        if (move != Vector2.zero)
        {
            if (move.y > 0) turn = true;
            if (move.y < 0) turn = false;

            if (canChangeState) ChangeState(State.Run, 0);
        }
        else
        {
            if (canChangeState) ChangeState(State.Idle, 0);
        }

        anim.SetBool("turn", turn);
        anim.SetInteger("state", (int)currentState);
    }
}
