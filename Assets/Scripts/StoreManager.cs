using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    private List<GameObject> _stores = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        var preExistingStores = GameObject.FindGameObjectsWithTag("Store");
        foreach (var s in preExistingStores)
        {
            _stores.Add(s);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject NearestStore(Vector3 position)
    {
        float shortestDist = 100000000;
        GameObject output = null;
        foreach (var s in _stores)
        {
            float dist = (position - s.transform.position).magnitude;
            if (dist < shortestDist)
            {
                shortestDist = dist;
                output = s;
            }
        }
        return output;
    }

    public void RegisterStore(GameObject store)
    {
        _stores.Add(store);
    }
}
