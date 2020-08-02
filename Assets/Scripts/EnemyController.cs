using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootPoint;
    public NavMeshAgent agent;
    GameObject target;
    public float lookRadius = 5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }
    private void Update()
    {
        //target = GameController.Instance().hero.transform.position;
        if (target != null) {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, target.transform.position, lookRadius);
            if (hit.collider.gameObject == target)
            {
                print("ПИФ-ПАФ");
                Shoot(target.transform.position);
            }
        }
    }
    private void Shoot(Vector2 destination) {
        GameObject b = GameObject.Instantiate(bullet, shootPoint.transform);
        b.GetComponent<Rigidbody2D>().AddRelativeForce(destination * 10f, ForceMode2D.Impulse);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("А Я ВСЁ ДУМАЛ, КОГДА ЖЕ ТЫ ПОЯВИШЬСЯ");
        if (collision.gameObject.GetComponent<HP>().race == HP.Race.Hero)
        {
            target = collision.gameObject;
            agent.SetDestination(target.transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        print("ТЫ КУДА?!");
        if (collision.gameObject.GetComponent<HP>().race == HP.Race.Hero)
        {
            target = null;
            Vector2 lastSeen = collision.transform.position;
            agent.SetDestination(lastSeen);
        }
    }
}
