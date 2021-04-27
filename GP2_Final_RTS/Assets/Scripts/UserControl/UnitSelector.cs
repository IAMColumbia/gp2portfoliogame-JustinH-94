using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
public class UnitSelector : MonoBehaviour
{
    [Header("Raycast")]
    public Camera cam;
    public LayerMask mask;

    [Header("Selection Box start/end Points")]
    public Vector3 startPos;
    public Vector3 endPos;

    [Header("Selection box Info")]
    public RectangleF selectionRect;
    public Vector3 rectCenter;
    public Vector3 rectSize;
    public Vector3 halfExtents;

    [Header("Selected units")]
    public List<GameObject> selectedUnits = new List<GameObject>();

    [Header("GameObject selection box")]
    public GameObject selectorBox;

    Vector3 moveToPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(selectedUnits.Count.ToString());
        if (Input.GetMouseButtonDown(0))
        {
            startPos = DoRay();
            SelectUnit();
            //startPos = DoRay();
        }
        if (Input.GetMouseButton(0))
        {
            SelectUnit();
            selectorBox.SetActive(true);
            endPos = DoRay();
            HandleRectangle();
            selectorBox.transform.position = rectCenter;
            selectorBox.transform.localScale = rectSize + new Vector3(0f, 1f, 0f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectorBox.SetActive(false);
            endPos = DoRay();
            HandleRectangle();
            SelectUnits();
        }
        if (Input.GetMouseButtonDown(1))
        {
            MoveUnits(DoRay());
        }
    }

    void HandleRectangle()
    {
        rectSize = startPos - endPos;
        rectSize.x = Mathf.Abs(rectSize.x);
        rectSize.y = Mathf.Abs(rectSize.y);
        rectSize.z = Mathf.Abs(rectSize.z);

        rectCenter = (startPos + endPos) * 0.5f;

        halfExtents = rectSize * 0.5f;
    }

    void ClearSelectedUnits()
    {
        foreach(GameObject g in selectedUnits)
        {
            UnitMove u = g.GetComponent<UnitMove>();
            u.DeSelectUnit();
        }
        selectedUnits.Clear();
    }

    void SelectUnit()
    {
        ClearSelectedUnits();

        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            if(hitInfo.collider.gameObject.name == "SkilledBuild")
            {
                Debug.Log("Selected");
                UnitMove u = hitInfo.collider.GetComponent<UnitMove>();
                u.SelectUnit();
                u.CalculateOffSet(rectCenter);
                hitInfo.collider.gameObject.GetComponent<SkilledBuild>().panel.SetActive(true);
                selectedUnits.Add(u.gameObject);
            }
        }
    }

    void SelectUnits()
    {
        ClearSelectedUnits();

        RaycastHit[] hit = Physics.BoxCastAll(rectCenter, halfExtents, Vector3.up);

        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("PlayerUnit"))
            {
                UnitMove u = hit[i].collider.GetComponent<UnitMove>();
                u.SelectUnit();
                u.CalculateOffSet(rectCenter);
                selectedUnits.Add(u.gameObject);
            }
        }
    }

    void MoveUnits(Vector3 pos)
    {
        moveToPos = pos;
        foreach(GameObject g in selectedUnits)
        {
            UnitMove u = g.GetComponent<UnitMove>();
            u.MoveToSpot(pos);
        }
    }

    Vector3 DoRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000f, mask))
        {
            if (hit.collider.CompareTag("Terrain"))
                return hit.point;
        }

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(rectCenter, rectSize);

        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(startPos, 0.5f);

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(endPos, 0.5f);

        Gizmos.color = UnityEngine.Color.cyan;
        Gizmos.DrawSphere(moveToPos, 0.5f);

        Gizmos.color = UnityEngine.Color.gray;
        Gizmos.DrawSphere(rectCenter, 0.5f);
    }
}
