using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [HideInInspector] public enum Race{
        Hero,
        Enemy
    }

    [Header("General")]
    public float hp;
    public Race race;
    

    //default methods

    //public methods
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            //death
        }
    }
}
