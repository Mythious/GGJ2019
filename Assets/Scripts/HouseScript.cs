using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HouseScript : MonoBehaviour
{
    [Header("People Spawn Ting on Tings")]
    public GameObject PersonPrefab;
    public int NumPeopleToSpawn;

    private BuildingScript _buildingScript;
    bool _built = false;

    // Use this for initialization
    void Start()
    {
        _buildingScript = gameObject.GetComponent<BuildingScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_built)
        {
            if(_buildingScript.Built())
            {
                _built = true;
                OnBuilt();
            }
        }
    }

    void OnBuilt()
    {
        var popManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<PopulationScript>();
        float angleChange = (2 * Mathf.PI) / NumPeopleToSpawn;
        for (int i = 0; i < NumPeopleToSpawn; i++)
        {
            float f = angleChange * i;
            Vector3 pos = new Vector3(Mathf.Cos(f), 0, Mathf.Sin(f)) * _buildingScript.inRadius;
            pos.y = 1.5f;
            var p = GameObject.Instantiate(PersonPrefab);
            p.GetComponent<NavMeshAgent>().enabled = false;
            p.transform.position = new Vector3(transform.position.x + pos.x, pos.y, transform.position.z + pos.z);
            p.GetComponent<NavMeshAgent>().enabled = true;
            popManager.AddPop(p);
        }
    }
}
