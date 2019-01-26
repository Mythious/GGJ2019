using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourceTypes;
using UnityEngine.AI;

public class Harvester : MonoBehaviour
{

    public int carryCapacity;
    public int amountHeld;
    public ResourceTypes typeHeld = ResourceTypes.NONE;
    public bool isGathering = false;
    public GameObject nodeHarvesting;
    public NavMeshAgent playerAgent;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GatherTick());
    }

    // Update is called once per frame
    void Update()
    {
        if (isGathering == true)
        {
            if (nodeHarvesting == null)
            {
                isGathering = false;
            }
             else if (amountHeld >= carryCapacity)
            {
                isGathering = false;
                nodeHarvesting.GetComponent<NodeManager>().gatherers--;
                //find nearest store

                //return to store
                //playerAgent.SetDestination()
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if (hitObject.tag == "resource")
        {
            NodeManager node = other.GetComponent<NodeManager>();
            nodeHarvesting = hitObject;
            node.gatherers++;
            if (typeHeld != ResourceTypes.NONE && typeHeld != node.resourceType)
            {
                amountHeld = 0;
            }
            typeHeld = node.resourceType;
            isGathering = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if (hitObject.tag == "resource")
        {
            NodeManager node = other.GetComponent<NodeManager>();
            nodeHarvesting = null;
            node.gatherers--;
        }
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            Gather();
            yield return new WaitForSeconds(1);
        }
    }

    public void Gather()
    {
        if (isGathering)
        {
            amountHeld++;
        }
    }

}
