using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Barracks : Building
{
    Text[] t;
    public Barracks()
    {
        Name = "Barracks";
        Health = 500;
        state = State.Active;
    }

    protected override void Start()
    {
        textUI = new Text[unitObject.Length];
        buttonText = new string[unitObject.Length];
        panel = GameObject.Find("Canvas/Barrack Panel");
        AddText();
        base.Start();

        for (int i = 0; i < t.Length; i++)
        {
            Debug.Log(t[i].name.ToString());
        }
        Debug.Log(Resources.FindObjectsOfTypeAll<Text>()[0]);
    }

    void AddText()
    {
        for(int i = 0; i < buttonText.Length; i++)
        {
            if (i == 0)
                buttonText[i] = "Canvas/Barrack Panel/Button/ Text";
            else if (i == 1)
                buttonText[i] = "Canvas/Barrack Panel/second button/ Text";
        }
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
        Debug.Log(GameObject.Find("Canvas/Barrack Panel/Button/Text").GetComponent<Text>().name);
        //unit1Text = GameObject.Find("Canvas/Barrack Panel/Button/Text").GetComponent<Text>();
        //unit1Text.text = unitObject[0].GetComponent<Unit>().name;
        //for (int i = 0; i < unitObject.Length; i++)
        //{
        //    if(i == 0)
        //        text[i] = GameObject.Find("Canvas/Barrack Panel/Button/Text").GetComponent<Text>();
        //    else if(i == 1)
        //        text[i] = GameObject.Find("Canvas/Barrack Panel/second button/Text").GetComponent<Text>();
        //    text[i].text = unitObject[i].GetComponent<Unit>().name;
        //}
    }
}
