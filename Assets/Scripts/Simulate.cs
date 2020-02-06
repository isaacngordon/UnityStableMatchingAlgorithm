using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulate : MonoBehaviour
{
    public float delay = 2;
    float startTime;
    float lastTimeStep;
    //externally defined fields
    public int totalPerGender;          //number of objects in each group
    public Transform groundCircle;      //reference to the circular floor
    public GameObject malePrefab;       //prefab for male GameObject
    public GameObject femalePrefab;     //prefab for female GameObject
    public Canvas nameCanvas;           //reference to UI canvas

    //internal fields
    GameObject[] males;                 //stores all male gameobjects
    GameObject[] females;               //stores all female gameobjects
    Vector3 center;                     //stores center of the circular floor
    float radius;                       //stores radius of the circular floor

    //global fields for matching algorithm
    int maleIterator = 0;                   
    public float timeInterval;
    bool ready = false;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastTimeStep = startTime;
        //define arrays of males and female game objects
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
            nameCanvas.GetComponent<ClampName>().generateNameLabel(males[i], "M " + i);
            males[i].GetComponent<Mate>().SetInitialPosition(malePos);

            //Instatiate female i and set name label
            females[i] = Instantiate(femalePrefab, femPos, rotation);
            nameCanvas.GetComponent<ClampName>().generateNameLabel(females[i], "F " + i);
            females[i].GetComponent<Mate>().SetInitialPosition(femPos);
        }

        //rate the opposite sex
        for(int i = 0; i < totalPerGender; i++)
        {
            males[i].GetComponent<Mate>().Rate(females);
            females[i].GetComponent<Mate>().Rate(males);

            males[i].GetComponent<Mate>().Rate(females);
            females[i].GetComponent<Mate>().Rate(males);
        }
        

        ready = true;
        print("Ready!!!!!");

    }

    // Update is called once per frame
    void Update()
    {
        if(!ready)
        {
            print("no go yet: " + (Time.time - startTime).ToString());
            return;
        }

        if (Time.time - lastTimeStep < delay) return;

        if(maleIterator < males.Length)
        {
            bool success = males[maleIterator].GetComponent<Mate>().FindSomePartner();

            if (success) maleIterator++;
        }
        else
        {
            maleIterator = 0;

            //for(int x = 0; x < totalPerGender; x++)
            //{
            //    GameObject m = males[x];
            //    GameObject f = m.GetComponent<Mate>().GetPartner();
            //    Debug.Log("MATCH>> Male: ");// + m.GetComponent<ClampName>().GetNameText() + " Female: " + f.GetComponent<ClampName>().GetNameText());
            //}
        }
    }

    Vector3 PointOnCircumference(float r, Vector3 origin, float angle)
    {
        float x = origin.x + (r * Mathf.Cos(angle));
        float z = origin.z + (r * Mathf.Sin(angle));

        return new Vector3(x, 2, z);
    }

    void GenerateNextMatch()
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
