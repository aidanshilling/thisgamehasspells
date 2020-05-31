using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public float ManaCost;
    public GameObject Player;
    public GameObject GameManager;
    public GameObject SpellEffect;

    // Start is called before the first frame update
    public void setup()
    {
        Player = PersistentManager.Instance.Player;
        GameManager = PersistentManager.Instance.GameManager;
    }
}
