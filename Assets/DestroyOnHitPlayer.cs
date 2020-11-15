using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHitPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerShip")
        {
            Destroy(gameObject);
        }
    }
}
