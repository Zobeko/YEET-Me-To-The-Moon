using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : AbstractShip
{

    public float delayBeforeFiring = 0.5f;


    protected void Awake()
    {
        base.Awake();
        if (GetComponent<AutoShooter>()) {
            GetComponent<AutoShooter>().enabled = false;
            StartCoroutine(WaitBeforeFiring());
        }
    }


    public override void OnDamageTaken(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            OnDeath();
            Debug.Log("damageTaken");
            return;
        }
    }

    protected override void OnDeath()
    {
        Debug.Log("death");
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        explosion.Play();
        Destroy(gameObject, explosion.main.duration);
        GameController.instance.OnEnemyDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damager damager = collision.gameObject.GetComponent<Damager>();

        if (damager && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            OnDamageTaken(damager.damageValue);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator WaitBeforeFiring()
    {
        yield return new WaitForSeconds(delayBeforeFiring);
        GetComponent<AutoShooter>().enabled = true;
    }

}
