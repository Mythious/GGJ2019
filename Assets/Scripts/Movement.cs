using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour {

    public LayerMask groundLayer;
    public NavMeshAgent playerAgent;
    [Header("ClickBloom")]
    public GameObject bloom;
    public float timeLive = 1;
    bool selected;

    private void Awake()
    {
        selected = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if(selected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hitPosition;

                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPosition, Mathf.Infinity, groundLayer))
                {
                    playerAgent.SetDestination(hitPosition.point);
                    if (bloom != null)
                    {
                        var bloomIn = GameObject.Instantiate(bloom);
                        bloomIn.transform.position = hitPosition.point;
                        Destroy(bloomIn, timeLive); 
                    }
                }
            }
        }
	}

    public void Selected(bool pSelected)
    {
        selected = pSelected;
    }
}
