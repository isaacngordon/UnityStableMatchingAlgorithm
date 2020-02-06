using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampName : MonoBehaviour
{

    public GameObject textPrefab;
    string theText = "null";
    
    public void generateNameLabel(GameObject obj, string nameString)
    {
        theText = nameString;
        Vector3 nextPosition = obj.transform.position;
        GameObject tempTextBox = Instantiate(textPrefab, nextPosition, transform.rotation);

        //Parent to the this canvas
        tempTextBox.transform.SetParent(this.transform, false);

        //set target and text
        tempTextBox.GetComponent<NameLabel>().setTarget(obj);
        tempTextBox.GetComponent<NameLabel>().setName(nameString);
    }

    public string GetNameText()
    {
        return theText;
    }
}
