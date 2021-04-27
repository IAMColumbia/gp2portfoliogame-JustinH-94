using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerClick : MonoBehaviour
{
    public GameObject[] gobj;
    public List<GameObject> listBuildings = new List<GameObject>();
    public GameObject[] panels;
    public Barracks barrack;
    public RadioStation radioS;
    public Base _base;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                
                TurnOffPanels();
                for(int i = 0; i < listBuildings.Count; i++)
                {
                    if(hitInfo.collider.gameObject.name == "Base")
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
                }

                //for (int i = 0; i < gobj.Length; i++)
                //{
                //    Debug.Log(hitInfo.collider.name.ToString());
                //    if (hitInfo.transform.gameObject == gobj[i])
                //    {
                //        TurnOffPanels();
                //        if (gobj[i].gameObject.name == barrack.name)
                //        {
                //            barrack.panel.SetActive(true);
                //            barrack.UnitOnButton();
                //        }
                //        else if (gobj[i].gameObject.name == radioS.name)
                //        {
                //            radioS.panel.SetActive(true);
                //            radioS.UnitOnButton();
                //        }
                //        else if (gobj[i].gameObject.name == _base.name)
                //        {
                //            _base.panel.SetActive(true);
                //            _base.UnitOnButton();
                //        }
                //        break;
                //    }
                //}
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
