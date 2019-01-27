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
    public NavMeshAgent playerAgent;
    public bool isGathering = false;
    GameObject nodeHarvesting = null;
    GameObject MapManager;

    // Use this for initialization
    void Start()
    {
        MapManager = GameObject.FindGameObjectWithTag("MapManager");
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
                StoreManager storeManager = MapManager.GetComponent<StoreManager>();
                GameObject nearestStore = storeManager.NearestStore(transform.position);
                Debug.Log("Nearest Store = " + nearestStore.transform.position);
                //return to store
                playerAgent.SetDestination(nearestStore.transform.position);
            }
            else if (amountHeld + nodeHarvesting.GetComponent<NodeManager>().gatherTick > carryCapacity )
            {
                isGathering = false;
                nodeHarvesting.GetComponent<NodeManager>().gatherers--;
                //find nearest store
                GameObject nearestStore = MapManager.GetComponent<StoreManager>().NearestStore(transform.position);
                Debug.Log("Nearest Store = " + nearestStore.transform.position);
                //return to store
                playerAgent.SetDestination(nearestStore.transform.position);
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
            if (typeHeld != node.resourceType)
            {
                amountHeld = 0;
            }
            typeHeld = node.resourceType;
            isGathering = true;
        }
        if (hitObject.tag == "Store" && nodeHarvesting != null && hitObject.GetComponent<BuildingScript>().Built())
        {
                playerAgent.SetDestination(nodeHarvesting.transform.position);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if (hitObject.tag == "resource")
        {
            NodeManager node = other.GetComponent<NodeManager>();
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
            int gatherAmount = nodeHarvesting.GetComponent<NodeManager>().gatherTick;
            if (amountHeld + gatherAmount <= carryCapacity)
            {
                amountHeld += gatherAmount;
            }
        }
    }

}
