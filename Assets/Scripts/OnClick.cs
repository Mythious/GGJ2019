using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnClick : MonoBehaviour
{

    public Material green;
    public Material blank;
    private MeshRenderer myRend;
    public bool isSelected;

    // Use this for initialization
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        isSelected = false;
        Camera.main.gameObject.GetComponent<ClickDetection>().selectableObjects.Add(this.gameObject);
        Click();
    }

    public void Click()
    {
        if (isSelected == false)
        {
            myRend.material = blank;
        }
        else
        {
            myRend.material = green;
        }
    }
}
