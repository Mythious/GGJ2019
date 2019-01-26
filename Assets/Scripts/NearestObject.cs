using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestObject : MonoBehaviour
{

    public Vector4 m_nearestObjectPosition = new Vector4(0, 0, 0, 1);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag("house");
        List<Transform> transforms = new List<Transform>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            transforms.Add(gameObjects[i].transform);
        }


        Transform tMin = null;
        float minDist = 0;
        Vector3 currentPos = transform.position;
        foreach (Transform t in transforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
                tMin = t;
                minDist = dist;
        }

        Shader.SetGlobalVector("_NearestObjectPosition", tMin.transform.position);
    }

   
}
