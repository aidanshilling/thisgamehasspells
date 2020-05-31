using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{

    public float health = 100;
    spellManager sm;
    public float mana;
    public Material manaMaterial;
    public GameObject manaInd;

    

    // Start is called before the first frame update
    void Start()
    {
        manaMaterial = manaInd.GetComponent<Renderer>().material;
        sm = GetComponent<spellManager>();
        mana = sm.percentMana;
    }

    private void Update()
    {
        mana = sm.percentMana;
        manaMaterial.SetFloat("_Cutoff", 1f - mana);

        if (health <= 0)
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.name);
        }
    }
}
