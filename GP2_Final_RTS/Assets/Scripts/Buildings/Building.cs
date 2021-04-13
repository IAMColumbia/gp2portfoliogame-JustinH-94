using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Building : MonoBehaviour
{
    protected enum State { Active, Destroyed }

    protected State state;

    public static Component component;
    public GameObject panel;
    public GameObject[] unitObject;
    public GameObject SpawnLoc;
    public Text[] text;
    protected string[] unitNames;
    protected string Name;
    protected int Health;
    protected bool isDestroyed;
    protected bool isBarrackSelected;
    protected bool isRadioSelected;
    public bool isPlayer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        isDestroyed = false;
        state = State.Active;
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
            a.transform.position = SpawnLoc.transform.position;
            if (isPlayer)
            {
                GameObject.Find("PlayerSystem").GetComponent<PlayerInfo>().numOfUnits.Add(a);
                a.GetComponent<Unit>().isPlayer = true;
            }
            else
            {
                GameObject.Find("EnemySystem").GetComponent<EnemySystem>().numOfUnits.Add(a);
                a.GetComponent<Unit>().isPlayer = false;
            }
        }
        else
        {
            Debug.Log("At Max Unit Capacity");
        }
    }

    public void UnitOnButton()
    {
        if (isBarrackSelected)
        {
            for (int i = 0; i < unitObject.Length; i++)
            {
                text[i].text = unitObject[i].GetComponent<Unit>().name;
            }
        }
    }

    public void BuildingSelected(bool isSelected, string building)
    {
        if (isSelected)
        {
            panel.SetActive(true);
            isBarrackSelected = true;
            UnitOnButton();
        }
        else
        {
            panel.SetActive(false);
            isBarrackSelected = false;
        }
    }


}
