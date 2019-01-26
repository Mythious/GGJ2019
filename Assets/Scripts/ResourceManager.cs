using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourceTypes;

public static class ResourceManager
{

    private static Dictionary<ResourceTypes, int> resources;

    /// <summary>
    /// Add some amount of a resource
    /// </summary>
    /// <param name="type"> the type of resource </param>
    /// <param name="amount"> the amount of resource to add </param>
    public static void AddResource(ResourceTypes type, int amount)
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

    /// <summary>
    /// Remove some amount of a resource
    /// </summary>
    /// <param name="type"> the type of resource </param>
    /// <param name="amount"> the amount of resource to remove </param>
    /// <returns> bool marks if there was enough resource to spend </returns>
    public static bool RemoveResource(ResourceTypes type, int amount)
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
