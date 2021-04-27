using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkilledBuild : Unit
{
    public GameObject[] buildings;
    //public GameObject[] UnitBuildPhase;
    public GameObject panel;
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
        this.transform.position = spawnLocation.transform.position;
        attackRange = 5f;
        moveSpeed = 5.0f;
        rotSpeed = 5.0f;
        health = 100;
        attackDamage = 5;
        buildTime = 0f;
        panel.SetActive(false);
        NameOnPanel();
        buildingBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildingBase.transform.position = new Vector3(0, 0, 0);
        base.Start();
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
        if (isBuilding)
        {
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
                isBuilding = false;
                this.transform.position = spawnLocation.transform.position;
                Destroy(this.gameObject);
            }
        }
    }

    public void CreateBuilding(int buildIndex)
    {
        isBuilding = true;
        index = buildIndex;
        buildingBase.transform.position = this.transform.position;
        this.transform.position = new Vector3(1000, -10, 0);
    }

    void Timer(){ buildTime += Time.deltaTime;}
}
