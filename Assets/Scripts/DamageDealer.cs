using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if(!(gameObject.GetComponent<Player_Laser>() == null))
        {
            if(IsPlayerOrEnemy())
            {
                
            }
            else if (gameObject.GetComponent<Player_Laser>().GetDestroyable())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (IsPlayerOrEnemy())
            {

            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        
    }

    private bool IsPlayerOrEnemy()
    {
        if(gameObject.GetComponent<Player>() || gameObject.GetComponent<Enemy>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}