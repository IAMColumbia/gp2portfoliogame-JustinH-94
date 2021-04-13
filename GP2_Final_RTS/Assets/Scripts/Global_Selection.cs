using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Selection : MonoBehaviour
{
    Selection_Dictionary select;
    RaycastHit hit;
    MeshCollider selectionBox;
    Mesh selectionMesh;
    bool dragSelect;

    Vector3 point1, point2;
    Vector3[] verts;
    Vector2[] corners;
    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<Selection_Dictionary>();
        dragSelect = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            point1 = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            if((point1 - Input.mousePosition).magnitude > 20)
            {
                Debug.Log("Pew");
                dragSelect = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!dragSelect)
            {
                
                Ray ray = Camera.main.ScreenPointToRay(point1);

                if (Physics.Raycast(ray, out hit))
                {
                    
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (hit.transform.gameObject.tag == "PlayerUnit" || hit.transform.gameObject.tag == "PlayerBase")
                        {
                            select.AddSelected(hit.transform.gameObject);
                        }
                        
                    }
                    else
                    {
                        select.DeSelectedAll();
                        if (hit.transform.gameObject.tag == "PlayerUnit" || hit.transform.gameObject.tag == "PlayerBase")
                        {
                            select.AddSelected(hit.transform.gameObject);
                        }
                    }
                }
                else
                {
                    select.DeSelectedAll();
                }
            }
            else
            {
                verts = new Vector3[4];
                int i = 0;
                point2 = Input.mousePosition;
                corners = GetBoundBox(point1, point2);
                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);
                    if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 7)))
                    {
                        verts[i] = new Vector3(hit.point.x, 0, hit.point.z);
                    }
                    i++;
                }
                selectionMesh = GenerateSelectMesh(verts);

                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    select.DeSelectedAll();
                }

                Destroy(selectionBox, 0.02f);
            }
            dragSelect = false;
        }
    }

    private void OnGUI()
    {
        if (dragSelect)
        {
            var rect = SelectRectangle.GetScreenRect(point1, Input.mousePosition);
            SelectRectangle.DrawScreenRect(rect, new Color(.8f, .8f, .95f, .25f));
            SelectRectangle.DrawScreenRectBorder(rect, 2, new Color(.8f, .8f, .95f));
        }
    }

    Vector2[] GetBoundBox(Vector2 p1, Vector2 p2)
    {
        Vector2 p_1;
        Vector2 p_2;
        Vector2 p_3;
        Vector2 p_4;

        if(p1.x < p2.x)
            if(p1.y > p2.y)
            {
                p_1 = p1;
                p_2 = new Vector2(p2.x, p1.y);
                p_3 = new Vector2(p1.x, p2.y);
                p_4 = p2;
            }
            else
            {
                p_1 = new Vector2(p1.x, p2.y);
                p_2 = p2;
                p_3 = p1;
                p_4 = new Vector2(p2.x, p1.y);
            }
        else
            if (p1.y > p2.y)
            {
                p_1 = new Vector2(p2.x, p1.y);
                p_2 = p2;
                p_3 = p1;
                p_4 = new Vector2(p1.x, p2.y);
            }
            else
            {
                p_1 = p2;
                p_2 = new Vector2(p1.x, p2.y);
                p_3 = new Vector2(p2.x, p1.y);
                p_4 = p1;
            }
        Vector2[] corners = {p_1, p_2, p_3, p_4 };
        return corners;
    }

    Mesh GenerateSelectMesh(Vector3[] corners)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 };

        for(int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }
        for(int  j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + Vector3.up * 100f;
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;
        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerUnit")
            select.AddSelected(other.gameObject);
    }
}
