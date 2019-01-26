using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingPlacementManager : MonoBehaviour
{
    [Header("House Types")]
    public List<GameObject> housePrefab = new List<GameObject>();

    public List<GameObject> buildings = new List<GameObject>();

    //[Header("RayCastingStuff")]
    private Camera camera;

    [Header("Gizmo Stuff")]
    public float Radius = 10;
    public Color Colour = new Color(1, 0, 0, 0.1f);
    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag != "Terrain")
                {
                    return;
                }
                // Do something with the object that was hit by the raycast.
                AddBuilding(housePrefab[0], hit.point);
            }
        }
    }

    void AddBuilding(GameObject prefab, Vector3 position)
    {
        BuildingScript bs = prefab.GetComponent<BuildingScript>();//heh
        if (bs == null || !CanBuild(position, bs.inRadius, bs.outRadius))
        {
            //TODO:: Sound "NOPE"
            return;
        }
        var newBuilding = GameObject.Instantiate(prefab, transform);
        newBuilding.transform.position = position + new Vector3(0, newBuilding.transform.localScale.y / 2, 0);
        buildings.Add(newBuilding);
    }

    bool CanBuild(Vector3 position, float inRadius, float outRadius)
    {
        bool isValid = false;
        foreach (var b in buildings)
        {
            BuildingScript bs = b.GetComponent<BuildingScript>();//heh
            Vector3 dif = b.transform.position - position;
            float magnitude = dif.magnitude;
            if (magnitude < bs.outRadius)
            {
                isValid = true;
            }
            if(magnitude < bs.inRadius + inRadius )
            {
                return false;
            }
        }

        return isValid;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Colour;
        foreach (var b in buildings)
        {
            BuildingScript bs = b.GetComponent<BuildingScript>();//heh
            if (bs != null)
            {
                Gizmos.DrawSphere(b.transform.position, bs.inRadius);
                Gizmos.DrawSphere(b.transform.position, bs.outRadius);
            }

        }
    }

}
