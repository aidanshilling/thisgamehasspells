using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{


    public GameObject lHand;
    public GameObject rHand;
    private Transform leftHand;
    private Transform rightHand;
    private float radius;
    public string nextLevel;

    private void Start()
    {
        radius = 2;
        leftHand = lHand.transform;
        rightHand = rHand.transform;
    }

    private void Update()
    {
        float ldistance = Vector3.Distance(leftHand.position, transform.position);
        float rdistance = Vector3.Distance(rightHand.position, transform.position);

        if (ldistance <= radius || rdistance <= radius)
        {
            Debug.Log("Scene Changing");
            SceneManager.LoadScene(nextLevel);
            Debug.Log("Scene Changed");
        }
    }

    // Start is called before the first frame update


}
