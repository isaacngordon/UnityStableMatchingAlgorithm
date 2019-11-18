using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulate : MonoBehaviour
{
    //externally defined fields
    public int totalPerGender;          //number of objects in each group
    public Transform groundCircle;      //reference to the circular floor
    public GameObject malePrefab;       //prefab for male GameObject
    public GameObject femalePrefab;     //prefab for female GameObject
    public Canvas nameCanvas;           //reference to UI canvas

    GameObject[] males;                 //stores all male gameobjects
    GameObject[] females;               //stores all female gameobjects
    Vector3 center;                     //stores center of the circular floor
    float radius;                       //stores radius of the circular floor

    //global fields for matching algorithm
    int maleIterator;                   
    public float timeInterval;


    // Start is called before the first frame update
    void Start()
    {
        males = new GameObject[totalPerGender];
        females = new GameObject[totalPerGender];

        radius = groundCircle.localScale.x / 2;
        center = groundCircle.position;

        //along the circumference the ground plane, instatiate male from 0 - PI, and females from PI-2PI
        float offset = 180 / totalPerGender;
        for (int i = 0; i < totalPerGender; i++)
        {
            //calculate random positions for new males and femalesa
            Vector3 malePos = PointOnCircumference(radius, center, i * offset);
            Vector3 femPos = PointOnCircumference(radius, center, (i * offset) + 180);
            Quaternion rotation = Quaternion.identity;

            //Instatiate male i and set name label
            males[i] = Instantiate(malePrefab, malePos, rotation);
            nameCanvas.GetComponent<ClampName>().generateNameLabel(males[i], "Man " + i);

            //Instatiate female i and set name label
            females[i] = Instantiate(femalePrefab, femPos, rotation);
            nameCanvas.GetComponent<ClampName>().generateNameLabel(females[i], "Female " + i);
        }

        //rate the opposite sex
        for(int i = 0; i < totalPerGender; i++)
        {
            males[i].GetComponent<Mate>().Rate(females);
            females[i].GetComponent<Mate>().Rate(males);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Use this instead of the 
    }

    Vector3 PointOnCircumference(float r, Vector3 origin, float angle)
    {
        float x = origin.x + (r * Mathf.Cos(angle));
        float z = origin.z + (r * Mathf.Sin(angle));

        return new Vector3(x, 2, z);
    }

    void Match()
    {
        //for m in males
            //m should propose to first f in potentialPartners who is not engaged
                //if f is not enagaged, m and f become enagegd
                //else if f prefers m to her partner m*, then f and m* break up and m enagages f
                //else continue

        //for m in males
            // m.findPartner() return 
    }
}
