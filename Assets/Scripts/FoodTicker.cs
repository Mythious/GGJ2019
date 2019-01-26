using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTicker : MonoBehaviour
{
    [Header("Food Attributes")]
    public float FoodPerPerson;
    public int Population;
    public float TickTime;

    [Header("Test Variables")]
    public int Food;



    private float _timeSinceLastTick = 0;
    // Use this for initialization
    void Start()
    {

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
        int foodDepletion = (int)(FoodPerPerson * Population);
        Food -= foodDepletion;
        if(Food < 0)
        {
            //KILL DEATH MURDER
            int popDepletion = (int)(Food / FoodPerPerson);
            Population += popDepletion;
            Food = 0;
            if(Population < 0)
            {
                Population = 0;
                //Ya dead son
            }
        }
    }
}
