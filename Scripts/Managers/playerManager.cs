using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    #region Singleton
    public static playerManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject player;
}
