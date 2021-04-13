using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Component : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnDestroy()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
