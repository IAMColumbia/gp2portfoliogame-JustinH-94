using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Base : Building
{
    public Base()
    {
        Name = "Base";
        Health = 500;
        state = State.Active;
    }
    protected override void Start()
    {
        textUI = new Text[unitObject.Length];
        panel = GameObject.Find("Canvas/Base Panel");
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        switch (state)
        {
            case State.Active:
                BuildingActive();
                break;
            case State.Destroyed:
                DestroyBuilding();
                break;
        }
        //UnitOnButton();
    }
    public override void UnitOnButton()
    {
        textUI = new Text[unitObject.Length];
        textUI[0] = GameObject.Find("Canvas/Base Panel/Button/SkilledBuild").GetComponent<Text>();
        textUI[0].text = unitObject[0].GetComponent<Unit>().name;
    }
}
