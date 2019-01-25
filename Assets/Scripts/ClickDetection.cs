using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickDetection : MonoBehaviour {

    public LayerMask clickablesLayer;
    public List<GameObject> selectableObjects;

    private List<GameObject> selectedObjects;
    private Vector3 mousePos1, mousePos2;

	// Use this for initialization
	void Awake ()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer))
            {
                OnClick onClickScript = rayHit.collider.GetComponent<OnClick>();

                if (Input.GetKey("left shift"))
                {
                    if (onClickScript.isSelected == false)
                    {
                        selectedObjects.Add(rayHit.collider.gameObject);
                        onClickScript.isSelected = true;
                        onClickScript.Click();
                    }
                    else
                    {
                        selectedObjects.Remove(rayHit.collider.gameObject);
                        onClickScript.isSelected = false;
                        onClickScript.Click();
                    }
                }
                else
                {
                    ClearSelected();

                    selectedObjects.Add(rayHit.collider.gameObject);
                    onClickScript.isSelected = true;
                    onClickScript.Click();
                }
            }
            else
            {
                if(!Input.GetKey("left shift"))
                {
                    ClearSelected();
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }
	}

    void SelectObjects()
    {
        List<GameObject> removedObjects = new List<GameObject>();

        if(!Input.GetKey("left shift"))
        {
            ClearSelected();
        }

        Rect selectionRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach(GameObject selectedObject in selectableObjects)
        {
            if (selectedObject != null)
            {
                if(selectionRect.Contains(Camera.main.WorldToViewportPoint(selectedObject.transform.position), true))
                {
                    selectedObjects.Add(selectedObject);
                    selectedObject.GetComponent<OnClick>().isSelected = true;
                    selectedObject.GetComponent<OnClick>().Click();
                }
            }
            else
            {
                removedObjects.Add(selectedObject);
            }
        }
        if(removedObjects.Count > 0)
        {
            foreach(GameObject removedObject in removedObjects)
            {
                selectableObjects.Remove(removedObject);
            }
            removedObjects.Clear();
        }
    }

    void ClearSelected()
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                if(obj != null)
                {
                    obj.GetComponent<OnClick>().isSelected = false;
                    obj.GetComponent<OnClick>().Click();
                }
            }
            selectedObjects.Clear();
        }
    }
}
