using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }

    public GameObject GameManager;
    public GameObject Player;
    public SteamVR_Input_Sources LeftHand;
    public SteamVR_Input_Sources RightHand;
    public bool isLeftHanded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isLeftHanded = false;
    }

    private void Start()
    {
        GameManager = gameObject;
    }
}
