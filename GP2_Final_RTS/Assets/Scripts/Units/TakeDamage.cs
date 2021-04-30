using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int Health;
    public void SetHealth(int health)
    {
        Health = health;
    }

    public void DamageTaken(int damage, GameObject go)
    {
        Health -= damage;
        if (Health <= 0)
        {
            foreach(GameObject g in UnitSelector.selectedUnits)
            {
                if(this.gameObject == g)
                    UnitSelector.selectedUnits.Remove(this.gameObject);
            }
            if (this.gameObject.tag == "EnemyBase")
                GameObject.Find("EnemySystem").GetComponent<EnemySystem>().numOfBuildings.Remove(this.gameObject);
            else if (this.gameObject.tag == "EnemyUnit")
            {
                GameObject.Find("EnemySystem").GetComponent<EnemySystem>().numOfUnits.Remove(this.gameObject);
                PlayerInfo.EnemiesInVicinity.Remove(this.gameObject);
            }
            else if (this.gameObject.tag == "PlayerUnit")
                GameObject.Find("PlayerSystem").GetComponent<PlayerInfo>().numOfUnits.Remove(this.gameObject);
            else if (this.gameObject.tag == "PlayerBase")
                GameObject.Find("PlayerSystem").GetComponent<PlayerInfo>().numOfBuildings.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
