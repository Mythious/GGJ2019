using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public float m_seedTree;
    public float m_seedRock;
    public float m_seedBerries;

    public float spawnChanceTree;
    public float spawnChanceBerry;
    public float spawnChanceRock;

    public float BerryToRockRatio;

    public GameObject m_terrain;
    public GameObject m_tree;
    public GameObject m_rock;
    public GameObject m_berries;

    // Use this for initialization
    void Start()
    {
        var width = m_terrain.transform.localScale.x;
        var height = m_terrain.transform.localScale.z;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float iNew = (float)i / 5;
                float jNew = (float)j / 5;
                var perlin = Mathf.PerlinNoise(m_seedTree + iNew, m_seedTree + jNew);

                if (perlin < spawnChanceTree)
                {
                    var newTree = Instantiate(m_tree);
                    newTree.transform.position = new Vector3((i * 10) - 500, 0, (j * 10) - 500);
                }
                else
                {
                    var rand = Random.Range(0.0f, 1.0f);

                    if (rand <= BerryToRockRatio)
                    {
                        var perlinBerry = Mathf.PerlinNoise(m_seedBerries + iNew, m_seedBerries + jNew);

                        if (perlinBerry < spawnChanceBerry)
                        {
                            var newBerry = Instantiate(m_berries);
                            newBerry.transform.position = new Vector3((i * 10) - 500, 0, (j * 10) - 500);
                        }
                    }
                    else
                    {
                        var perlinRock = Mathf.PerlinNoise(m_seedRock + iNew, m_seedRock + jNew);

                        if (perlinRock < spawnChanceRock)
                        {
                            var newRock = Instantiate(m_rock);
                            newRock.transform.position = new Vector3((i * 10) - 500, 0, (j * 10) - 500);
                        }
                    }
                }
            }
        }
    }
}
