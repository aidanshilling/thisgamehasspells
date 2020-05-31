using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpell : SpellBase
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            Player.transform.position = this.transform.position;

            Debug.Log("collision with ground.");
            Destroy(gameObject);
            Player.GetComponent<spawnSpell>().count = false;
            Instantiate(SpellEffect, Player.transform);
        }
    }
    void Start()
    {
        ManaCost = 10.00f;
        setup();
    }
}
