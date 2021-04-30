using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Building : MonoBehaviour
{
    protected enum State { Active, Destroyed }

    protected State state;

    public static Component component;
    protected GameObject panel;
    public GameObject[] unitObject;
    public GameObject SpawnLoc;
    protected Text[] textUI;
    protected Text[] textNames;
    protected string Name;
    protected int Health;
    protected bool isDestroyed;
    protected bool isBarrackSelected;
    protected bool isRadioSelected;
    protected bool creatingUnit;
    protected float buildTime;
    protected int index;
    public bool isPlayer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isDestroyed = false;
        textNames = Resources.FindObjectsOfTypeAll<Text>();
        for(int i =0; i < textNames.Length; i++)
        {
            textNames[i].text = textNames[i].name;
        }
        state = State.Active;
        //SpawnLoc.transform.position = new Vector3(this.transform.position.x-5f, this.transform.position.y, this.transform.position.z);
        GetComponent<TakeDamage>().SetHealth(Health);
    }
    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected bool BuildingDestroyed()
    {
        if (Health <= 0)
            return true;
        return false;
    }

    protected void BuildingActive()
    {
        if (BuildingDestroyed())
            state = State.Destroyed;
    }

    protected void DestroyBuilding()
    {
        if(Health <= 0)
            Destroy(this.gameObject);
    }

    public virtual void CreateUnit(int unitIndex)
    {
        if (!PlayerInfo.atUnitCap || !EnemySystem.atUnitCap)
        {
            GameObject a = Instantiate(unitObject[unitIndex]) as GameObject;
            if (this.gameObject.tag == "PlayerBase")
            {
                GameObject.Find("PlayerSystem").GetComponent<PlayerInfo>().numOfUnits.Add(a);
                a.GetComponent<Unit>().isPlayer = true;
            }
            else if (this.gameObject.tag == "EnemyBase")
            {
                GameObject.Find("EnemySystem").GetComponent<EnemySystem>().numOfUnits.Add(a);
                a.GetComponent<Unit>().isPlayer = false;
                a.transform.position = SpawnLoc.transform.position;
            }
        }
        else
        {
            Debug.Log("At Max Unit Capacity");
        }
    }

    public virtual void UnitOnButton()
    {
    }
}
