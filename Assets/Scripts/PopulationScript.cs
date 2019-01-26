using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationScript : MonoBehaviour
{
    private int _currentPopulation;
    private List<GameObject> _population = new List<GameObject>();
    public Text populationText;

    // Use this for initialization
    void Start()
    {
        var workers = GameObject.FindGameObjectsWithTag("Worker");
        foreach(GameObject worker in workers)
        {
            AddPop(worker);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPop(GameObject personToAdd)
    {
        _currentPopulation++;
        _population.Add(personToAdd);
        UpdatePopulationCount();
    }

    public void RemovePop()
    {
        _currentPopulation--;
        int val = (int)(Random.value * (_population.Count - 1));
        var toDelete = _population[val];
        _population.RemoveAt(val);
        Destroy(toDelete);
        UpdatePopulationCount();
    }

    public int CurrentPopulation()
    {
        return _currentPopulation;
    }

    private void UpdatePopulationCount()
    {
        populationText.text = "Population : " + CurrentPopulation();
    }
}
