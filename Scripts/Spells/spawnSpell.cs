using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class spawnSpell : MonoBehaviour
{
    private GameObject spell;
    public GameObject hand;
    public float spellSpeed;
    public float spawnDistance = 0.5f;
    public float spellRadius = 1f;
    public bool isInRad = false;
    public GameObject gm;
    private float manaCost;
    spellManager sm;
    private float timer = 0;
    GameObject NewSpell;
    public bool count = false;


    private void Start()
    {
        spell = spellManager.instance.currentSpell;
        sm = gm.GetComponent<spellManager>();
    }


    void resetTimer()
    {
        timer = 0;
    }
    void tick()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        spell = spellManager.instance.currentSpell;
        manaCost = sm.manaCost;

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand) && !isInRad && sm.mana >= manaCost && count == false)
        {
            sm.mana -= manaCost;
        

            Vector3 spawnPos = hand.transform.position + hand.transform.right * spawnDistance;
            //Spawn spell in the players hand.
            NewSpell = (Instantiate(spell, spawnPos, hand.transform.rotation));
            //Create Node on the players right hand where the spell will become a child of said node.
            NewSpell.GetComponent<Rigidbody>().useGravity = false;
            NewSpell.transform.parent = hand.transform;
            //When player picks up spell gravity tuns back on
            resetTimer();
            count = true;
        }
        else if (!SteamVR_Input._default.inActions.GrabPinch.GetLastStateDown(SteamVR_Input_Sources.LeftHand))
        {
            tick();
        }

        if(timer >= 3)
        {
            if(sm.mana < sm.maxMana)
            {
                sm.mana += 0.1f;
            }
        }

        if (count)
        {
            float distance = Vector3.Distance(NewSpell.transform.position, hand.transform.position);

            if (distance > spellRadius)
            {
                NewSpell.transform.parent = NewSpell.transform;
                NewSpell.GetComponent<Rigidbody>().useGravity = true;
                isInRad = false;
                count = false;
            }
            else
            {
                isInRad = true;
            }
        }
    }
}