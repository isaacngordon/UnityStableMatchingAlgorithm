using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameLabel : MonoBehaviour
{
    GameObject target;
    int shiftUp = 2;
    int shiftRight = 5;

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint( target.transform.position + new Vector3(shiftRight, shiftUp, 0) );
        this.transform.position = namePos;
    }

    public void setName(string someName)
    {
        this.GetComponent<Text>().text = someName;
    }

    public void setTarget(GameObject someObj)
    {
        this.target = someObj;
    }
}
