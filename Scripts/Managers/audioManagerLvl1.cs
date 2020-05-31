using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerLvl1 : MonoBehaviour
{
    public float audioRadius;
    public GameObject player;
    public AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        audioRadius = 5;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > audioRadius)
        {
            speaker.Play();
        }
    }
}
