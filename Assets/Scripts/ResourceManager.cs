using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourceTypes;

public class ResourceManager : MonoBehaviour
{

    private Dictionary<ResourceTypes, int> resources = new Dictionary<ResourceTypes, int>();

    /// <summary>
    /// Add some amount of a resource
    /// </summary>
    /// <param name="type"> the type of resource </param>
    /// <param name="amount"> the amount of resource to add </param>
    public void AddResource(ResourceTypes type, int amount)
    {
        if (resources.ContainsKey(type))
        {
            resources[type] += amount;
        }
        else
        {
            resources.Add(type, amount);
        }
    }

    public int GetResourceLevel(ResourceTypes type)
    {
        return resources[type];
    }

    /// <summary>
    /// Remove some amount of a resource
    /// </summary>
    /// <param name="type"> the type of resource </param>
    /// <param name="amount"> the amount of resource to remove </param>
    /// <returns> bool marks if there was enough resource to spend </returns>
    public bool RemoveResource(ResourceTypes type, int amount)
    {
        if (resources.ContainsKey(type))
        {
            if (resources[type] > 0 + amount)
            {
                resources[type] -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


}
