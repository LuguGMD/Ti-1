using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthSystem : MonoBehaviour
{

    public float maxHealth;

    private float m_Health;
    public float currentHealth
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

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        //ANIMATING
        transform.DOScale(0.8f, 0.1f).OnComplete(ReturnScale);

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

    private void ReturnScale()
    {
        transform.DOScale(1f, 0.1f);
    }

    public void Heal(int healing)
    {
        //ANIMATING
        transform.DOScale(1.2f, 0.1f).OnComplete(ReturnScale);

        //Healing
        currentHealth += healing;

        //Clamping Health
        if(currentHealth >= maxHealth) 
        { 
            currentHealth = maxHealth;
        }
    }

}
