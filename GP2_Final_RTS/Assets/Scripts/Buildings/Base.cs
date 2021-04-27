using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Base : Building
{
    bool setActive;
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
        panel.SetActive(false);
        AddText();
        base.Start();
    }

    void AddText()
    {
        buttonText[0] = "/Canvas/Base Panel/Button /Text";
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
        textUI[0] = GameObject.Find("Canvas/Base Panel/Button/Text").GetComponent<Text>();
        textUI[0].text = unitObject[0].GetComponent<Unit>().name;
    }
}
