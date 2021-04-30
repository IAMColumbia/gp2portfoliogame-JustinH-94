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
        panel = GameObject.Find("Canvas/RS Panel");
        //panel.SetActive(false);
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
        for (int i = 0; i < textNames.Length; i++)
        {
            if (textNames[i].name == "BirdPlane")
                textNames[i].text = "BirdPlane";
            if (textNames[i].name == "BruiserCruiser")
                textNames[i].text = "BruiserCruiser";
        }
    }
}
