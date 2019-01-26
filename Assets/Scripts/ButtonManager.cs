using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button cancelButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void BuildingButton()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        BuildingScript buildScript = camera.GetComponent<BuildingScript>();


        if (clickedButton.name != "CancelButton")
        {
            // enable the X button
            cancelButton.gameObject.SetActive(true);

            // enable the building zones
            buildScript.selectionButtonPressed = true;
        }

        switch (clickedButton.name)
        {
            case "BuildingButton(1)":

                break;
            case "BuildingButton(2)":

                break;
            case "BuildingButton(3)":

                break;
            case "BuildingButton(4)":

                break;
            case "CancelButton":
                cancelButton.gameObject.SetActive(false);
                // disable the building zones
                buildScript.selectionButtonPressed = false;
                break;
            default:
                break;
        }
    }

}
