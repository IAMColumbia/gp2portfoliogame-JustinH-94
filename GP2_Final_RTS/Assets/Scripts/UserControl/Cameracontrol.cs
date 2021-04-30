using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour
{
    Vector3 pos;
    float moveSpeed = 30f;
    float ScreenEdge = 10.0f;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.transform.position;
        Move();
        ZBoundary();
        XBoundary();
    }

    void Move()
    {
        if (Input.mousePosition.y >= Screen.height - ScreenEdge)
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y <= Screen.height - Screen.height + ScreenEdge)
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x <= Screen.width - Screen.width + ScreenEdge)
            pos.z -= moveSpeed * Time.deltaTime;
        else if (Input.mousePosition.x >= Screen.width - ScreenEdge)
            pos.z += moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    void ZBoundary()
    {
        if(this.gameObject.transform.position.z >= 75)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 75);
        }
        else if (this.gameObject.transform.position.z <= -15)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -15);
        }
    }

    void XBoundary()
    {
        if (this.gameObject.transform.position.x <= -60)
        {
            this.gameObject.transform.position = new Vector3(-60, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else if (this.gameObject.transform.position.x >= 85)
        {
            this.gameObject.transform.position = new Vector3(85, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
    }
}
