using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootPoint;
    public NavMeshAgent agent;
    Vector2 target;
    public float lookRadius = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }
    void Update()
    {
        target = GameController.Instance().hero.transform.position;
        float distance = Vector2.Distance(target, transform.position);
        RaycastHit2D hit;
        if (distance <= lookRadius)
        {
            hit = Physics2D.Raycast(transform.position, target, lookRadius);
            if (hit.collider.gameObject == GameController.Instance().hero)
                Shoot(target);
            agent.SetDestination(target);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
    void Shoot(Vector2 destination) {
        GameObject b = GameObject.Instantiate(bullet, shootPoint.transform);
        b.GetComponent<Rigidbody2D>().AddForce(destination * 10f, ForceMode2D.Impulse);
    }
}
