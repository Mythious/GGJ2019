﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.ResourceTypes;


public class BuildingPlacementManager : MonoBehaviour
{
    [Header("House Types")]
    public List<GameObject> housePrefab = new List<GameObject>();

    public List<GameObject> buildings = new List<GameObject>();

    [Header("Layer Mask")]
    public LayerMask layerMask;

    //[Header("RayCastingStuff")]
    private Camera camera;

    [Header("Gizmo Stuff")]
    public float Radius = 10;
    public Color Colour = new Color(1, 0, 0, 0.1f);

    [HideInInspector]
    public bool selectionButtonPressed;
    public int BuildingToBuildIndex = 0;
    ResourceManager rm = null;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rm = GameObject.FindGameObjectWithTag("MapManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectionButtonPressed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (!EventSystem.current.IsPointerOverGameObject(-1))
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.transform.gameObject.tag != "Terrain")
                        {
                            return;
                        }
                        // Do something with the object that was hit by the raycast.
                        AddBuilding(housePrefab[BuildingToBuildIndex], hit.point);
                    }
                }
            }
        }
    }

    public void AddBuilding(GameObject prefab, Vector3 position)
    {
        BuildingScript bs = prefab.GetComponent<BuildingScript>();//heh
        if (bs == null || !CanBuild(position, bs.inRadius, bs.outRadius))
        {
            //TODO:: Sound "NOPE"
            return;
        }
        var newBuilding = GameObject.Instantiate(prefab, null);
        newBuilding.transform.position = position + new Vector3(0, 0, 0);
        if (BuildingToBuildIndex == 0)
        {
            rm.RemoveResource(ResourceTypes.WOOD, 50);
            rm.RemoveResource(ResourceTypes.FOOD, 10);
        }
        else if (BuildingToBuildIndex == 1)
        {
            rm.RemoveResource(ResourceTypes.WOOD, 75);
            rm.RemoveResource(ResourceTypes.STONE, 25);
        }
        buildings.Add(newBuilding);
    }

    bool CanBuild(Vector3 position, float inRadius, float outRadius)
    {
        bool isValid = false;
        if (BuildingToBuildIndex == 0)
        {
            if (!(rm.GetResourceLevel(ResourceTypes.WOOD) >= 50 && rm.GetResourceLevel(ResourceTypes.FOOD) >= 10))
            { 
                return false;
            }
        }
        else if (BuildingToBuildIndex == 1)
        {
            if (!(rm.GetResourceLevel(ResourceTypes.WOOD) >= 75 && rm.GetResourceLevel(ResourceTypes.STONE) >= 25))
            {
                return false;
            }
        }


        if (buildings.Count == 0)
        {
            return true;
        }
        foreach (var b in buildings)
        {
            BuildingScript bs = b.GetComponent<BuildingScript>();//heh
            Vector3 dif = b.transform.position - position;
            float magnitude = dif.magnitude;
            if (magnitude < bs.outRadius)
            {
                isValid = true;
            }
            if (magnitude < bs.inRadius + inRadius)
            {
                return false;
            }
        }


        return isValid;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Colour;
        foreach (var b in buildings)
        {
            BuildingScript bs = b.GetComponent<BuildingScript>();//heh
            if (bs != null)
            {
                Gizmos.DrawSphere(b.transform.position, bs.inRadius);
                Gizmos.DrawSphere(b.transform.position, bs.outRadius);
            }

        }
    }

}
