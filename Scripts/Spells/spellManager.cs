using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class spellManager : MonoBehaviour
{
    #region Singleton
    public static spellManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject teleportSpell;
    public GameObject freezeSpell;
    public GameObject combustionSpell;
    public GameObject currentSpell;
    public GameObject hand;
    public GameObject spellIndicator;
    private GameObject spell;
    private Renderer rend;
    public Material freezeMat;
    public Material telMat;
    public Material combMat;
    private int count;
    public float mana;
    public float manaCost;
    public float maxMana;
    public float percentMana;

    private void Start()
    {
        currentSpell = teleportSpell;
        count = 0;
        manaCost = 10;
        maxMana = mana;
        float spawnDistance = 0.1f;
        Vector3 spawnPos = hand.transform.position + hand.transform.forward * spawnDistance;
        spell = Instantiate(spellIndicator, spawnPos, hand.transform.rotation);
        spell.transform.parent = hand.transform;
        rend = spell.GetComponent<Renderer>();
        rend.sharedMaterial = telMat;
    }

    public float getPercentMana (float currentMana, float maxMana)
    {
        float percentMana;
        percentMana = currentMana / maxMana;        
        return percentMana;
    }

    // Update is called once per frame
    void Update()
    {

        percentMana = getPercentMana(mana, maxMana);

        if (SteamVR_Input._default.inActions.SwitchSpell.GetLastStateDown(SteamVR_Input_Sources.LeftHand) && count == 0)
        {
            manaCost = 25;
            currentSpell = freezeSpell;
            count++;

            rend.sharedMaterial = freezeMat;
        }
        else if (SteamVR_Input._default.inActions.SwitchSpell.GetLastStateDown(SteamVR_Input_Sources.LeftHand) && count == 1)
        {
            manaCost = 10;
            currentSpell = teleportSpell;
            count++;

            rend.sharedMaterial = telMat;
        }
        else if (SteamVR_Input._default.inActions.SwitchSpell.GetLastStateDown(SteamVR_Input_Sources.LeftHand) && count == 2)
        {
            manaCost = 50;
            currentSpell = combustionSpell;
            count -= 2;

            rend.sharedMaterial = combMat;
        }
    }
}
