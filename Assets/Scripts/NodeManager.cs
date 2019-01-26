using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourceTypes;

public class NodeManager : MonoBehaviour
{
    public ResourceTypes resourceType;
    public float harvestTime;
    public float availableResource;

    public bool harvesting;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("ResourceTick");
    }

    // Update is called once per frame
    void Update()
    {
        if (availableResource <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ResourceTick()
    {
        yield return new WaitForSeconds(1);
        GatherResource();
    }

    public void GatherResource()
    {
        //while (true)
        //{
        //    if (harvesting)
        //    {
        //        availableResource--;
        //        ResourceManager.AddResource(resourceType, 1);
        //    }
        //}
    }
}
