using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShipController : AbstractShip
{

    public float acceleration;
    public float mvSpeed;
    public float drag; 
    public float invulnerableDuration;

    public float driftSpeed;

    private bool invulnerable = false;

    private bool rightGunner = true;
    private bool leftGunner = true;

    private Rigidbody2D rBody;
    private GameController controller;

    protected void Awake()
    {
        base.Awake();
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller = GameController.instance;
    }

    private void Update()
    {
        if (rightGunner)
        {
            if (!controller.rightGunnerBool)
            {
                transform.Find("RightShooter").GetComponent<AutoShooter>().Unajust();
                rightGunner = false;
            }
        }

        if (leftGunner)
        {
            if (!controller.leftGunnerBool)
            {
                transform.Find("LeftShooter").GetComponent<AutoShooter>().Unajust();
                rightGunner = false;
            }
        }

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

        Vector2 mvDirection = new Vector2(mvX,mvY) * controller.reversedControls;

        if(mvDirection.magnitude > 0)
        {
            //On pousse le vaisseau dans la direction voulue
            rBody.velocity = Vector3.ClampMagnitude(rBody.velocity + mvDirection * acceleration * Time.fixedDeltaTime, mvSpeed);
        } else
        {
            //Le vaisseau ralentit grâce au drag
            //rBody.velocity -= rBody.velocity.normalized * drag * Time.fixedDeltaTime;
        }

        if (!controller.leftEngineerBool)
        {
            rBody.velocity = Vector3.ClampMagnitude(rBody.velocity - (Vector2) transform.right * driftSpeed, mvSpeed);
        }

        if (!controller.rightEngineerBool)
        {
            rBody.velocity = Vector3.ClampMagnitude(rBody.velocity + (Vector2)transform.right * driftSpeed, mvSpeed);
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
        SceneManager.LoadScene("GameOverScene");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damager damager = collision.gameObject.GetComponent<Damager>();

        if (damager && collision.gameObject.CompareTag("Enemy"))
        {
            OnDamageTaken(damager.damageValue);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator InvulnerableTimeCoroutine()
    {
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

}
