using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportTo : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject teleportEffect;
    public AudioSource sfx;

    private void OnCollisionEnter(Collision col)
    {
        if (thePlayer.GetComponent<spawnSpell>().isInRad == false)
        {
            if (col.gameObject.tag == "ground")
            {
                thePlayer.transform.position = this.transform.position;

                Debug.Log("collision with line.");
                sfx.Play();
                Destroy(gameObject);
                thePlayer.GetComponent<spawnSpell>().count = false;
                Instantiate(teleportEffect, thePlayer.transform);
            }
        }
    }
}
