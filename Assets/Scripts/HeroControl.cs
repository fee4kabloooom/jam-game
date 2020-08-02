using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControl : MonoBehaviour
{
    [Header("Moving")]
    public float moveSpeed;

    [Header("Attack")]
    public GameObject slash;
    public float slashCooldown;

    private Vector2 move;
    private Rigidbody2D rb;
    private AnimationController ac;
    private float slashStarted;

    //default methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<AnimationController>();
    }
    private void FixedUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Slash();
        }
    }

    //public methods

    //private methods
    private void Slash()
    {
        if (Time.time < slashStarted + slashCooldown) return;

        ac.ChangeState(AnimationController.State.Attack, 0.5f);
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg; ;
        slash.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        slashStarted = Time.time;
    }
}
