using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyShip
{

    public float mvSpeed = 2;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * 2;
    }

}
