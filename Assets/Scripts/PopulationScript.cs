using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationScript : MonoBehaviour
{
    private int _currentPopulation;
    private List<GameObject> _population = new List<GameObject>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPop(GameObject personToAdd)
    {
        _currentPopulation++;
        _population.Add(personToAdd);
    }

    public void RemovePop()
    {
        _currentPopulation--;
        int val = (int)(Random.value * (_population.Count - 1));
        var toDelete = _population[val];
        _population.RemoveAt(val);
        Destroy(toDelete);
    }

    public int CurrentPopulation()
    {
        return _currentPopulation;
    }
}
