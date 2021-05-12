using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerClick : MonoBehaviour
{
    public List<GameObject> listBuildings = new List<GameObject>();
    public GameObject[] panels;
    public Barracks barrack;
    public RadioStation radioS;
    public Base _base;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            Debug.Log(hitInfo.collider.transform.position);
            if (hit)
            {
                Debug.Log(hitInfo.collider.name.ToString());
                TurnOffPanels();
                for(int i = 0; i < listBuildings.Count; i++)
                {
                    Debug.Log(panels[i].name);
                    if (hitInfo.collider.gameObject.name == "Base")
                    {
                        if(panels[i].name == "Base Panel")
                            panels[i].SetActive(true);
                        if(listBuildings[i].name == "Base")
                            listBuildings[i].GetComponent<Building>().UnitOnButton();
                    }
                    if (hitInfo.collider.gameObject.name == "Barracks")
                    {
                        if (panels[i].name == "Barrack Panel")
                            panels[i].SetActive(true);
                        if (listBuildings[i].name == "Barracks")
                            listBuildings[i].GetComponent<Building>().UnitOnButton();
                    }
                    if (hitInfo.collider.gameObject.name == "RadioStation")
                    {
                        if (panels[i].name == "RS Panel")
                            panels[i].SetActive(true);
                        if (listBuildings[i].name == "RS Panel")
                            listBuildings[i].GetComponent<Building>().UnitOnButton();
                    }
                }
            }
        }
    }

    void TurnOffPanels()
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }
}
