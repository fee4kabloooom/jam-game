using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float lifeTime = 2f;
    float bulletSpeed = 5f;
    //bool deflected;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.right * bulletSpeed * Time.fixedDeltaTime); // какого чёрта не работает transform.forward?
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HP>()) collision.gameObject.GetComponent<HP>().TakeDamage(1);
    }
}
