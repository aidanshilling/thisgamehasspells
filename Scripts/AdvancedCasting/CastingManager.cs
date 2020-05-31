using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CastingManager : MonoBehaviour
{

    public GameObject CurrentSpell;
    public GameObject[] Spells;
    public SteamVR_Input_Sources CastingHand;
    public SteamVR_Input_Sources ChamberingHand;
    public GameObject Hand;
    public GameObject SpellNode;

    private bool SpellChambered;
    private bool CastingReady;
    private bool SpellIsChild;

    private int SpellCounter;
    private float CastRadius;

    void SwitchSpell()
    {
        if (SpellCounter < Spells.Length - 1)
        {
            SpellCounter++;
        }
        else
        {
            SpellCounter = 0;
        }
        
        CurrentSpell = Spells[SpellCounter];
    }

    void ChamberSpell()
    {
        // Instantiate current spell on a node in the players hand
        GameObject NewSpell = Instantiate(CurrentSpell, SpellNode.transform.position, SpellNode.transform.rotation);
        NewSpell.GetComponent<Rigidbody>().useGravity = false;
        NewSpell.transform.parent = SpellNode.transform;
        SpellIsChild = true;
        SpellChambered = true;
    }

    void UnChamberSpell()
    {
        if (SpellIsChild)
        {
            Destroy(SpellNode.transform.GetChild(0).gameObject);
        }
        SpellIsChild = false;
        SpellChambered = false;
    }

    void CastSpell()
    {
        // Casts a spell when a casting ready hand moves through a chambered spell
        SpellNode.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
        SpellNode.transform.GetChild(0).transform.parent = null;
        SpellIsChild = false;
        print("Spell has been cast");
    }

    void ReadyCasting()
    {
        // Prepares hand for spell casting
        CastingReady = true;
    }

    void UnReadyCasting()
    {
        // Relax's hand
        CastingReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpellChambered = false;
        SpellCounter = 0;
        CastRadius = 1f;
        
        CurrentSpell = Spells[SpellCounter];

        if (PersistentManager.Instance.isLeftHanded)
        {
            CastingHand = PersistentManager.Instance.LeftHand;
            Hand = PersistentManager.Instance.Player.transform.GetChild(0).transform.GetChild(1).gameObject;
            SpellNode = PersistentManager.Instance.Player.transform.GetChild(0).transform.GetChild(2).transform.GetChild(5).gameObject;
            ChamberingHand = PersistentManager.Instance.RightHand;
        }
        else if (!PersistentManager.Instance.isLeftHanded)
        {
            CastingHand = PersistentManager.Instance.RightHand;
            Hand = PersistentManager.Instance.Player.transform.GetChild(0).transform.GetChild(2).gameObject;
            SpellNode = PersistentManager.Instance.Player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(5).gameObject;
            ChamberingHand = PersistentManager.Instance.LeftHand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Hand.transform.position, SpellNode.transform.position);

        if (SteamVR_Input._default.inActions.SwitchSpell.GetLastStateDown(ChamberingHand))
        {
            SwitchSpell();
        }
        if (SteamVR_Input._default.inActions.Squeeze.GetAxis(ChamberingHand) == 1 && !SpellChambered && !SpellIsChild)
        {
            print("Down");
            ChamberSpell();
        }
        if (SteamVR_Input._default.inActions.Squeeze.GetAxis(ChamberingHand) == 0 && SpellChambered)
        {
            print("Up");
            UnChamberSpell();
        }
        if (SteamVR_Input._default.inActions.Squeeze.GetAxis(CastingHand) == 1 && !CastingReady)
        {
            // Ready's the hand for casting
            print("Casting Ready");
            ReadyCasting();
        }
        if (SteamVR_Input._default.inActions.Squeeze.GetAxis(CastingHand) == 0 && CastingReady)
        {
            // Ready's the hand for casting
            print("Hand Rested");
            UnReadyCasting();
        }
        if (distance < CastRadius && SpellChambered && CastingReady && SpellIsChild)
        {
            // Casts spell when readied casting hand moves through a chambered spell
            CastSpell();
        }
    }
}
