using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class freezeSpell : MonoBehaviour
{

    Renderer rend;
    public Material mat;
    public GameObject player;
    public AudioSource sfx;

    private void OnCollisionEnter(Collision col)
    {
        if (player.GetComponent<spawnSpell>().isInRad == false)
        {
            if (col.gameObject.tag == "enemy")
            {
                GameObject target = col.gameObject;


                foreach (Rigidbody rb in target.GetComponentsInChildren<Rigidbody>()) rb.isKinematic = true;

                target.GetComponent<NavMeshAgent>().isStopped = true;
                target.GetComponent<Animator>().enabled = false;

                rend = target.GetComponentInChildren<Renderer>();
                rend.sharedMaterial = mat;
                target.GetComponent<EnemyController>().isLiving = false;
                Debug.Log("collision with line.");
                player.GetComponent<spawnSpell>().count = false;
                sfx.Play();
                Destroy(gameObject);
                if (target.GetComponentInChildren<AudioSource>() != null)
                {
                    target.GetComponentInChildren<AudioSource>().enabled = false;
                }

            }
        }
    }
}
