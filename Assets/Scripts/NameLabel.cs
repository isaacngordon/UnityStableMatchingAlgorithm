using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameLabel : MonoBehaviour
{
    GameObject target;
    public int shiftUp = 2;
    public int shiftRight = 10;

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint( target.transform.position  );
        this.transform.position = namePos + new Vector3(shiftRight, shiftUp, 0);
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
