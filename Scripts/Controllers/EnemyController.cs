using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    public float lookAngle = 45f;
    public float attackRadius = 1f;
    public float attackAngle = 160f;
    Transform target;
    NavMeshAgent agent;
    private Animator anim;
    public GameObject gm;
    private playerStats ps;
    public bool isLiving;

    public bool isFacingPlayer(float maxAngle)
    {
        Vector3 directionBetween = (target.position - transform.position).normalized;
        directionBetween.y *= 0;
        float angle = Vector3.Angle(directionBetween, transform.forward);
        if(angle <= maxAngle)
        {
            return true;
        }
        return false;
    }

    public bool isInRange(float maxRadius)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= maxRadius)
        {
            return true;
        }
        return false;
    }
    
    public bool canAttack (float maxAttackRange, float maxAttackRadius)
    {
        bool flagOne = false;
        bool flagTwo = false;
        bool mainflag = false;
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 directionBetween = (target.position - transform.position).normalized;
        directionBetween.y *= 0;
        float angle = Vector3.Angle(directionBetween, transform.forward);
        if (distance <= maxAttackRange)
        {
            flagOne = true;
        }
        if (angle <= maxAttackRadius)
        {
            flagTwo = true;
        }
        if (flagOne == true && flagTwo == true)
        {
            mainflag = true;
        }

        return mainflag;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = playerManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ps = gm.GetComponent<playerStats>();
        isLiving = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        bool inSight = isFacingPlayer(lookAngle);
        bool inRange = isInRange(lookRadius);
        bool attackable = canAttack(attackRadius, attackAngle);
        if (inSight && inRange)
        {
            anim.SetTrigger("startWalking");
            faceTarget();
            agent.SetDestination(target.position);
            
            if (attackable)
            {
                ps.health -= 25;
                if (distance < 1.3)
                {
                    anim.SetTrigger("stopWalking");
                }
            }
        }
        if (!isLiving)
        {
            die();
        }
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void die ()
    {
        Destroy(transform.GetComponent<CapsuleCollider>());
        Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
