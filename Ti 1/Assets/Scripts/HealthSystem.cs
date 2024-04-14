using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth;

    private int m_Health;
    public int currentHealth
    {
        get { return m_Health; }
        set 
        {
            //Clamping the health value
            Mathf.Clamp(value, 0, maxHealth);
            m_Health = value;
        }
    }
    
    public bool isDead = false;

    public bool TakeDamage(int damage)
    {
        //Taking the damage
        currentHealth -= damage;

        //Looking if health is over
        if(currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
        }

        //Returning if it's dead
        return isDead;
    }

    public void Heal(int healing)
    {
        //Healing
        currentHealth += healing;

        //Clamping Health
        if(currentHealth >= maxHealth) 
        { 
            currentHealth = maxHealth;
        }
    }

}
