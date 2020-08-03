using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleZone : MonoBehaviour
{
    public float lifeSpan = 0.5f;
    private void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0f) Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HP>().race == HP.Race.Hero) collision.gameObject.GetComponent<HP>().TakeDamage(10);
    }
}
