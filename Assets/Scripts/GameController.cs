using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController _gc;
    public static GameController Instance() { return _gc; }
    private void Awake()
    {
        _gc = this;
    }
    #endregion

    public GameObject hero;
}
