using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    private StoreManager _storeManager;
    private BuildingScript _buildingScript;
    private ResourceManager _resourceManager;
    bool _built = false;
    // Use this for initialization
    void Start()
    {
        GameObject mapManager = GameObject.FindGameObjectWithTag("MapManager");
        _storeManager = mapManager.GetComponent<StoreManager>();
        _resourceManager = mapManager.GetComponent<ResourceManager>();
        _buildingScript = GetComponent<BuildingScript>();
        _built = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_built)
        {
            if (_buildingScript.Built())
            {
                OnBuilt();
                _built = true;
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_built)
        {
            if(other.gameObject.tag == "Worker")
            {
                //Pass reasources
                Harvester h = other.gameObject.GetComponent<Harvester>();
                if(h != null)
                {
                    Debug.Log("Depsitiong resources");
                    _resourceManager.AddResource(h.typeHeld, h.amountHeld);
                }
            }
        }
    }

    private void OnBuilt()
    {
        _storeManager.RegisterStore(gameObject);
    }
}
