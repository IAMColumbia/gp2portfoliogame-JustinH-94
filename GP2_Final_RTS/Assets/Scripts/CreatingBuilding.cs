using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingBuilding : MonoBehaviour
{
    GameObject baseOfBuilding;
    public GameObject[] buildings;
    public PlayerClick addBuildingToList = new PlayerClick();
    float buildTime;
    bool isCreating;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        buildTime = 0f;
        isCreating = false;
    }

    // Update is called once per frame
    void Update()
    {
        CreateBuilding();
        //Debug.Log(isCreating.ToString());
    }

    void CreateBuilding()
    {
        if (isCreating)
        {
            Debug.Log(buildTime.ToString());
            Timer();
            if (5f < buildTime && buildTime < 10f)
            {
                baseOfBuilding.transform.localScale = new Vector3(5, 5, 5);
            }
            if (buildTime > 15f)
            {
                GameObject a = Instantiate(buildings[index]) as GameObject;
                a.name = buildings[index].name;
                a.transform.position = new Vector3(baseOfBuilding.transform.position.x, 8.6839f, baseOfBuilding.transform.position.z);
                addBuildingToList.listBuildings.Add(a);
                Destroy(baseOfBuilding);
                Destroy(this.gameObject);
            }
        }
    }

    public void StartCreateBuilding(Vector3 position, int _index)
    {
        baseOfBuilding = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube)) as GameObject;
        baseOfBuilding.transform.position = position;
        isCreating = true;
        index = _index;
    }

    void Timer() { buildTime += Time.deltaTime; }
}
