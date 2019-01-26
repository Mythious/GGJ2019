using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{


    [Header("Build param")]
    public float inRadius = 10;
    public float outRadius = 50;
    public float BuildTime = 2;
    [Header("Spheres")]
    public GameObject innerSphere;
    public GameObject outerSphere;

    private GameObject[] _radiusIndicators;
    private bool _built = false;
    private float _endYScale;
    private float _timeBuilt = 0;
    // Use this for initialization
    void Start()
    {
        _endYScale = transform.localScale.y;
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        _radiusIndicators = new GameObject[2];
        _radiusIndicators[0] = Instantiate(innerSphere, transform);
        float inScale = inRadius / 5;
        _radiusIndicators[0].transform.localScale = new Vector3(inScale, 0.1f, inScale);
        float halfScale = transform.localScale.y / 2;
        _radiusIndicators[0].transform.position -= new Vector3(0, halfScale, 0);

        _radiusIndicators[1] = Instantiate(outerSphere, transform);
        float outScale = outRadius / 5;
        _radiusIndicators[1].transform.localScale = new Vector3(outScale, 0.1f, outScale);
        _radiusIndicators[1].transform.position -= new Vector3(0, halfScale, 0);

        _built = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Shows building zones
        if (Input.GetKey(KeyCode.S))
        {
            _radiusIndicators[0].SetActive(true);
            _radiusIndicators[1].SetActive(true);
        }
        else
        {
            _radiusIndicators[0].SetActive(false);
            _radiusIndicators[1].SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_built)
        {
            if (other.gameObject.tag == "Worker")
            {
                _timeBuilt += 1 * Time.deltaTime;
                float yScale = (_endYScale / BuildTime) * _timeBuilt;
                transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, yScale / 2, transform.position.z);
                if (_timeBuilt > BuildTime)
                {
                    _built = true;
                }
            }
        }
    }

    public bool Built()
    {
        return _built;
    }
}
