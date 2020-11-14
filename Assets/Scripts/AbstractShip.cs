using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractShip : MonoBehaviour
{

    public int maxHealth = 1;
    protected int health;

    protected void Awake()
    {
        health = maxHealth;
    }

    public abstract void OnDamageTaken(int amount);

    protected abstract void OnDeath();

}
