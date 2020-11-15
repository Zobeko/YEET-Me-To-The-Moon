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

    public AudioSource audioSourceLowHealth = null;
    public AudioSource audioSourceEffects = null;
    public AudioClip playerExplosionClip = null;
    public AudioClip playerLowHealthClip = null;
    public bool isHealthLow = true;

    public SpriteRenderer mainRenderer;

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

        
        
        if (health <= 0.25 * maxHealth && isHealthLow){
            isHealthLow = false;
            StartCoroutine(LowHealthSoudCoroutine());
        }
        else
        {
            StopCoroutine(LowHealthSoudCoroutine());
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //On récupère les touches de déplacements directionnel
        float mvX = Convert.ToInt32(Input.GetButton("Right")) - Convert.ToInt32(Input.GetButton("Left"));
        float mvY = Convert.ToInt32(Input.GetButton("Up")) - Convert.ToInt32(Input.GetButton("Down"));

        Vector2 mvDirection = new Vector2(mvX,mvY) * controller.reversedControls;

        if(mvDirection.magnitude > 0)
        {
            //On pousse le vaisseau dans la direction voulue
            rBody.velocity = Vector3.ClampMagnitude(rBody.velocity + mvDirection * acceleration * Time.fixedDeltaTime, mvSpeed);
        } else
        {
            //Le vaisseau ralentit grâce au drag
            //rBody.velocity -= rBody.velocity.normalized * drag * Time.fixedDeltaTime;
            rBody.velocity = Vector2.zero;
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
        audioSourceEffects.PlayOneShot(playerExplosionClip);
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
            StartCoroutine("colliderFlash");
        }
    }

    IEnumerator collideFlash()
    {
        
        Color c = mainRenderer.color;
        mainRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        mainRenderer.material.color = c;
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

    IEnumerator LowHealthSoudCoroutine()
    {
        audioSourceLowHealth.PlayOneShot(playerLowHealthClip);
        yield return new WaitForSeconds(4.3f);
        isHealthLow = true;
    }

}
