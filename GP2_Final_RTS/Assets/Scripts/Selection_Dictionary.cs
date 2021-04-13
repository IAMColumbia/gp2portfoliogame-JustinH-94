using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Dictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedObj = new Dictionary<int, GameObject>();

    public void AddSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedObj.ContainsKey(id)))
        {
            selectedObj.Add(id, go);
            go.AddComponent<Selection_Component>();
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void DeSelected(int id)
    {
        Destroy(selectedObj[id].GetComponent<Selection_Component>());
        selectedObj.Remove(id);
    }

    public void DeSelectedAll()
    {
        foreach(KeyValuePair<int, GameObject> pair in selectedObj)
        {
            if (pair.Value != null)
                Destroy(selectedObj[pair.Key].GetComponent<Selection_Component>());
        }
        selectedObj.Clear();
    }
}
