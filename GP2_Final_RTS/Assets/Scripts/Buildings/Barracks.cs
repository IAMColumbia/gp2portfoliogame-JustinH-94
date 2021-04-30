using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Barracks : Building
{
    public Barracks()
    {
        Name = "Barracks";
        Health = 500;
        state = State.Active;
    }

    protected override void Start()
    {
        textUI = new Text[unitObject.Length];
        panel = GameObject.Find("Canvas/Barrack Panel");
        base.Start();
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

    public override void UnitOnButton()
    {
        for(int i = 0; i < textNames.Length; i++)
        {
            if(textNames[i].name == "FearDaSphere")
                textNames[i].text = "FearDaSphere";
            if (textNames[i].name == "BlockingBlock")
                textNames[i].text = "BlockingBlock";
        }
    }
}
