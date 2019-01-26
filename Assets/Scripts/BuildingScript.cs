using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public bool selectionButtonPressed = false;

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
    private BuildingPlacementManager _buildingPlacementManager;
    // Use this for initialization
    void Start()
    {
        _endYScale = transform.localScale.y;
        transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        _radiusIndicators = new GameObject[2];
        _radiusIndicators[0] = Instantiate(innerSphere, null);
        float inScale = inRadius * 2;
        _radiusIndicators[0].transform.localScale = new Vector3(inScale, 0.1f, inScale);
        //float halfScale = _endYScale / 2;
        _radiusIndicators[0].transform.position = transform.position;

        _radiusIndicators[1] = Instantiate(outerSphere, null);
        float outScale = outRadius * 2;
        _radiusIndicators[1].transform.localScale = new Vector3(outScale, 0.1f, outScale);
        _radiusIndicators[1].transform.position = transform.position;

        _built = false;

        _buildingPlacementManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<BuildingPlacementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shows building zones
        //if (Input.GetKey(KeyCode.S))
        if (_buildingPlacementManager.selectionButtonPressed)
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
                Debug.Log(_timeBuilt);
                float yScale = (_endYScale / BuildTime) * _timeBuilt;
                if(yScale < 0.5f)
                {
                    yScale = 0.5f;
                }
                transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, yScale / 2, transform.position.z);
                if (_timeBuilt > BuildTime)
                {
                    _built = true;
                    Debug.Log("Built");
                }
            }
        }
    }

    public bool Built()
    {
        return _built;
    }
}
