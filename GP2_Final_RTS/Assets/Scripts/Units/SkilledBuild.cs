using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkilledBuild : Unit
{
    public GameObject[] buildings;
    //public GameObject[] UnitBuildPhase;
    public GameObject panel;
    public Button[] btn;
    public Text[] text;
    GameObject buildingBase;
    bool isBuilding;
    int index; //index of the building in buildings array
    // Start is called before the first frame update
    protected override void Start()
    {
        name = "SkilledBuild";
        damageType = "Melee";
        attackSpeed = 1.5f;
        attackRange = 5f;
        moveSpeed = 5.0f;
        rotSpeed = 5.0f;
        health = 100;
        attackDamage = 5;
        buildTime = 0f;
        NameOnPanel();
        buildingBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildingBase.transform.position = new Vector3(0, 0, 0);
        GetPanel();
        isBuilding = false;
        panel.SetActive(true);
        SetButtons();
        base.Start();
    }
    void SetButtons()
    {
        btn = Resources.FindObjectsOfTypeAll<Button>();
        for(int i =0; i < btn.Length; i++)
        {
            if (btn[i].name == "BaseCreate")
                btn[i].onClick.AddListener(delegate { CreateBuilding(0); });
            else if (btn[i].name == "BarrackCreate")
                btn[i].onClick.AddListener(delegate { CreateBuilding(1); });
            else if (btn[i].name == "RadioStationCreate")
                btn[i].onClick.AddListener(delegate { CreateBuilding(2); });
        }
    }

    void GetPanel()
    {
        for (int i = 0; i < Resources.FindObjectsOfTypeAll<GameObject>().Length; i++)
        {
            if(Resources.FindObjectsOfTypeAll<GameObject>()[i].name == "SkilledBuild_Panel")
                panel = Resources.FindObjectsOfTypeAll<GameObject>()[i];
        }
    }

    void NameOnPanel()
    {
        for(int i = 0; i < buildings.Length; i++)
        {
            text[i].text = buildings[i].name;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Building();
    }

    void Building()
    {
        Debug.Log(isBuilding);
        if (isBuilding)
        {
            this.transform.position = new Vector3(240, 10, 222);
            panel.SetActive(false);
            Timer();
            if(5f < buildTime && buildTime < 10f)
            {
                buildingBase.transform.localScale = new Vector3(5, 5, 5);
            }
            if(buildTime > 15f)
            {
                GameObject a = Instantiate(buildings[index]) as GameObject;
                a.name = buildings[index].name;
                a.transform.position = new Vector3(buildingBase.transform.position.x, 16.5f,buildingBase.transform.position.z);
                Destroy(buildingBase);
                Destroy(this.gameObject);
            }
        }
    }

    public void CreateBuilding(int buildIndex)
    {
        isBuilding = true;
        index = buildIndex;
        buildingBase.transform.position = this.transform.position;
        this.transform.position = new Vector3(120,13,104);
        Debug.Log(buildIndex);
    }

    void Timer(){ buildTime += Time.deltaTime;}
}
