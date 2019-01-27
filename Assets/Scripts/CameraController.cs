using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float movementSpeed = 20.0f;
    public float borderThickness = 10f;
    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;
    public Vector2 screenLimit;
    public Transform startPos;

	// Use this for initialization
	void Start () {
        Vector3 pos = transform.position;
        pos.z = startPos.position.z - 30;
        pos.x = startPos.position.x;
        pos.y = 40f;
        transform.position = pos;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = transform.position;

	    if(Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - borderThickness)
        {
            pos.z += movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= borderThickness)
        {
            pos.z -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - borderThickness)
        {
            pos.x += movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= borderThickness)
        {
            pos.x -= movementSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -screenLimit.x, screenLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -screenLimit.y, screenLimit.y);

        transform.position = pos;
	}
}
