using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnClick : MonoBehaviour
{
    public bool isSelected;
    public GameObject selectionGraphic;

    // Use this for initialization
    void Start()
    {
        isSelected = false;
        Camera.main.gameObject.GetComponent<ClickDetection>().selectableObjects.Add(this.gameObject);
        Click();
    }

    public void Click()
    {
        if (isSelected == false)
        {
            selectionGraphic.SetActive(false);
        }
        else
        {
            selectionGraphic.SetActive(true);
        }
    }
}
