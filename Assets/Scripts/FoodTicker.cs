using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTicker : MonoBehaviour
{
    [Header("Food Attributes")]
    public float FoodPerPerson;
    public float TickTime;

    [Header("Test Variables")]
    public int Food;



    private float _timeSinceLastTick = 0;
    private PopulationScript _popScript;
    // Use this for initialization
    void Start()
    {
        _popScript = GetComponent<PopulationScript>();
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
        int foodDepletion = (int)(FoodPerPerson * _popScript.CurrentPopulation());
        Food -= foodDepletion;
        if(Food < 0)
        {
            //KILL DEATH MURDER
            int popDepletion = (int)(Food / FoodPerPerson);
            for(int i = 0; i < Mathf.Abs(popDepletion); i++)
            {
                _popScript.RemovePop();
            }
            Food = 0;
            if(_popScript.CurrentPopulation() < 0)
            {
                //Ya dead son
            }
        }
    }
}
