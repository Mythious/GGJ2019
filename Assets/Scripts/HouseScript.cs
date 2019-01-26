using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 pos = new Vector3(Mathf.Cos(f), 1.5f, Mathf.Sin(f)) * _buildingScript.inRadius;
            var p = GameObject.Instantiate(PersonPrefab);
            p.transform.position = pos + transform.position;
            popManager.AddPop(p);
        }
    }
}
