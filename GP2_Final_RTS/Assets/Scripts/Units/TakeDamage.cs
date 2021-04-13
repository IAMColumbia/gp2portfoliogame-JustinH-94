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
        if(this.gameObject.tag == "EnemyUnit" || this.gameObject.tag == "PlayerUnit")
        {
            this.gameObject.GetComponent<Unit>().isAttacked = true;
            this.gameObject.GetComponent<Unit>().attackingUnit = go;
        }
        if (Health <= 0)
        {
            if (this.gameObject.tag == "EnemyBase")
                EnemySystem.numOfBuildings.Remove(this.gameObject);
            else if (this.gameObject.tag == "EnemyUnit")
            {
                GameObject.Find("EnemySystem").GetComponent<EnemySystem>().numOfUnits.Remove(this.gameObject);
                PlayerInfo.EnemiesInVicinity.Remove(this.gameObject);
            }
            else if (this.gameObject.tag == "PlayerUnit")
                GameObject.Find("PlayerSystem").GetComponent<PlayerInfo>().numOfUnits.Remove(this.gameObject);
            else if (this.gameObject.tag == "PlayerBase")
                PlayerInfo.numOfBuildings.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
