using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTicker : MonoBehaviour
{
    [Header("Food Attributes")]
    public float FoodPerPerson;
    public float TickTime;

    //[Header("Test Variables")]
    //public int Food;



    private float _timeSinceLastTick = 0;
    private PopulationScript _popScript;
    private ResourceManager _resourceManager;
    // Use this for initialization
    void Start()
    {
        _popScript = GetComponent<PopulationScript>();
        _resourceManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastTick += Time.deltaTime;
        if(_timeSinceLastTick > TickTime)
        {
            TickFood();
            _timeSinceLastTick = 0;
        }
    }

    void TickFood()
    {
        int Food = _resourceManager.GetResourceLevel(Assets.Scripts.ResourceTypes.ResourceTypes.FOOD);
        int foodDepletion = (int)(FoodPerPerson * _popScript.CurrentPopulation());
        _resourceManager.AddResource(Assets.Scripts.ResourceTypes.ResourceTypes.FOOD, -foodDepletion);
        Food = _resourceManager.GetResourceLevel(Assets.Scripts.ResourceTypes.ResourceTypes.FOOD);
        if (Food < 0)
        {
            //KILL DEATH MURDER
            int popDepletion = (int)(Food / FoodPerPerson);
            for(int i = 0; i < Mathf.Abs(popDepletion); i++)
            {
                _popScript.RemovePop();
            }
            _resourceManager.AddResource(Assets.Scripts.ResourceTypes.ResourceTypes.FOOD, -Food);
            if (_popScript.CurrentPopulation() < 0)
            {
                //Ya dead son
            }
        }
    }
}
