using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeSpan = 2f;
    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0f) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
