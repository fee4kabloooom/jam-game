using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("General")]
    public float lookRadius = 5f;

    [Header("Attack")]
    public float shootCooldown;
    public GameObject shootPoint;
    public GameObject shootPointHandler;
    public GameObject bulletPref;

    [Header("AI")]
    public Vector3[] points;

    NavMeshAgent agent;
    GameObject target;
    EnemyAnimationController ac;
    Vector3 lastPoint;
    float shootStarted;
    int currentPoint = 0;
    bool checkedPoint;

    //default methods
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        ac = GetComponent<EnemyAnimationController>();
    }
    private void Update()
    {
        if(target != null)
        {
            print("shoot");
            Shoot();
        }
        else if(!checkedPoint)
        {
            print("checked point");
            agent.SetDestination(lastPoint);
            if (Vector2.Distance(transform.position, lastPoint) < 0.2f) checkedPoint = true;
        }
        else
        {
            if (Vector2.Distance(transform.position, points[currentPoint]) < 1f) GetRandomPoint();
            else
            {
                agent.SetDestination(points[currentPoint]);
                print(Vector2.Distance(transform.position, points[currentPoint]));
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HP>().race == HP.Race.Hero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position - transform.position);
            if(hit.collider.tag == "Player")
            {
                target = collision.gameObject;
                print("can see player");
            }
            print(hit.collider.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            print("cant see player");
            lastPoint = target.transform.position;
            target = null;
            checkedPoint = false;
        }
    }

    //public methods

    //private methods
    private void Shoot()
    {

        if(Time.time >= shootCooldown + shootStarted)
        {
            print("fire2");
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
            shootPointHandler.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            Instantiate(bulletPref, shootPoint.transform.position, Quaternion.identity);
            shootStarted = Time.time;
            ac.ChangeState(EnemyAnimationController.State.Attack, 1);
        }
    }
    private void GetRandomPoint()
    {
        print("random point");
        List<Vector3> allPoints = new List<Vector3>();
        for (int i = 0; i < points.Length; i++) if (i != currentPoint) allPoints.Add(points[i]);
        currentPoint = Random.Range(0, allPoints.Count); 
    }
}
