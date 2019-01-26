using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourceTypes;

public class NodeManager : MonoBehaviour
{
    public ResourceTypes resourceType;
    public float harvestTime;
    public float availableResource;

    public int gatherers;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ResourceTick());
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
        while (true)
        {
            GatherResource();
            yield return new WaitForSeconds(1);
        }
    }

    public void GatherResource()
    {
        if (gatherers > 0)
        {
            availableResource -= gatherers;
        }
    }
}
