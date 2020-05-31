using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class combustionSpell : MonoBehaviour
{
    public float overLapRadius = 5f;
    public float force;
    bool hasExploded = false;
    public GameObject player;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        force = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && player.GetComponent<spawnSpell>().isInRad == false)
        {
            Explode();
        }
    }

    void Explode ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, overLapRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Rigidbody[] rbc = nearbyObject.GetComponentsInChildren<Rigidbody>();
            Animator an = nearbyObject.GetComponent<Animator>();
            CharacterJoint[] joints = nearbyObject.GetComponentsInChildren<CharacterJoint>();
            EnemyController en = nearbyObject.GetComponent<EnemyController>();
            if (an != null)
            {
                an.enabled = false;
                nearbyObject.GetComponent<NavMeshAgent>().isStopped = true;
              
                if (nearbyObject.GetComponentInChildren<AudioSource>() != null && an != null)
                {
                    nearbyObject.GetComponentInChildren<AudioSource>().enabled = false;
                }
            }
            if (en != null)
            {
                en.isLiving = false;
            }
            if (rbc != null && joints != null)
            {
                
                foreach (CharacterJoint jt in joints)
                {
                    jt.enablePreprocessing = false;
                    jt.enableProjection = true;
                }

                foreach (Rigidbody childrenRig in rbc)
                {
                    childrenRig.isKinematic = false;

                    //childrenRig.AddExplosionForce(force, transform.position, overLapRadius, 3.0f);
                }
            }
           
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, overLapRadius);
            }

        }

        sfx.Play();
        Destroy(gameObject);
        
        player.GetComponent<spawnSpell>().count = false;
        hasExploded = true;
        
    }
}
