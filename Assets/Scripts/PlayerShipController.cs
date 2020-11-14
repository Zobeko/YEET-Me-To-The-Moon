using System;
using System.Collections;
using UnityEngine;

public class PlayerShipController : AbstractShip
{

    public float acceleration;
    public float mvSpeed;
    public float drag; 
    public float invulnerableDuration;

    private bool invulnerable = false;

    private Rigidbody2D rBody;

    protected void Awake()
    {
        base.Awake();
        rBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //On récupère les touches de déplacements directionnel
        float mvX = Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a"));
        float mvY = Convert.ToInt32(Input.GetKey("w")) - Convert.ToInt32(Input.GetKey("s"));

        Vector2 mvDirection = new Vector2(mvX,mvY);

        if(mvDirection.magnitude > 0)
        {
            //On pousse le vaisseau dans la direction voulue
            rBody.velocity = Vector3.ClampMagnitude(rBody.velocity + mvDirection * acceleration * Time.fixedDeltaTime, mvSpeed);
        } else
        {
            //Le vaisseau ralentit grâce au drag
            rBody.velocity -= rBody.velocity.normalized * drag * Time.fixedDeltaTime;
        }
    }

    //Appelé lorsque des dégâts sont pris
    public override void OnDamageTaken(int amount)
    {
        if (!invulnerable)
        {
            health -= amount;
            if (health <= 0)
            {
                OnDeath();
                return;
            }
            invulnerable = true;
            StartCoroutine(InvulnerableTimeCoroutine());
        }
    }

    protected override void OnDeath()
    {
        //Do stuff
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damager damager = collision.gameObject.GetComponent<Damager>();

        if (damager && collision.gameObject.CompareTag("Enemy"))
        {
            OnDamageTaken(damager.damageValue);
        }
    }

    IEnumerator InvulnerableTimeCoroutine()
    {
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

}
