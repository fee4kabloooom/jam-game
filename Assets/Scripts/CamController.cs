using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [Header("General")]
    public float camSensivity;

    private Camera cam;

    //default methods
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 desiredPos = GameController.Instance().hero.transform.position + (mousePos - GameController.Instance().hero.transform.position)*camSensivity;
        transform.position = new Vector3(desiredPos.x, desiredPos.y, -10);
    }
}
