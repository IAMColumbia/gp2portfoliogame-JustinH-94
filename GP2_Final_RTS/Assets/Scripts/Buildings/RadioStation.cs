using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadioStation : Building
{
    public RadioStation()
    {
        Name = "Radio Station";
        Health = 400;
        state = State.Active;
        
    }

    protected override void Start()
    {
        panel.SetActive(false);
        AddText();
        base.Start();
    }

    void AddText()
    {
        textUI[0] = GameObject.Find("/Canvas/RS Panel/Button/Text").GetComponent<Text>();
        textUI[1] = GameObject.Find("/Canvas/RS Panel/second button/Text").GetComponent<Text>();
    }
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
}
