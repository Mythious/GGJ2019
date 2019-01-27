using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ResourceTypes;
using System;

public class ResourceManager : MonoBehaviour
{

    private Dictionary<ResourceTypes, int> resources = new Dictionary<ResourceTypes, int>();
    public Text StoneText, WoodText, FoodText;
    private GameOverHandler gameOverHandler;

    public void Start()
    {
        gameOverHandler = GameObject.FindGameObjectWithTag("MapManager").GetComponent<GameOverHandler>();
        foreach (ResourceTypes e in Enum.GetValues(typeof(ResourceTypes)))
        {
            resources.Add(e, 0);
        }

        AddResource(ResourceTypes.FOOD, 200);
        AddResource(ResourceTypes.WOOD, 75);
        AddResource(ResourceTypes.STONE, 25);
    }
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
            UpdateResource(type);
        }
        else
        {
            resources.Add(type, amount);
            UpdateResource(type);
        }
    }

    public int GetResourceLevel(ResourceTypes type)
    {
        if (!resources.ContainsKey(type))
        {
            resources.Add(type,0);
        }
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
                UpdateResource(type);
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

    /// <summary>
    /// update the resources in the UI
    /// </summary>
    /// <param name="inputType"></param>
    private void UpdateResource(ResourceTypes inputType)
    {
        switch (inputType)
        {
            case ResourceTypes.FOOD:
                if (FoodText != null)
                {
                    FoodText.text = "Food : " + resources[inputType];
                    gameOverHandler.DeathByFood(resources[inputType]);
                }
                break;
            case ResourceTypes.STONE:
                if (StoneText != null)
                {
                    StoneText.text = "Stone : " + resources[inputType];
                }
                break;
            case ResourceTypes.WOOD:
                if (WoodText != null)
                {
                    WoodText.text = "Wood : " + resources[inputType];
                }
                break;
        }

    }

}
