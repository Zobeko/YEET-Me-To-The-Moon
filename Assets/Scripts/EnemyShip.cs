using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : AbstractShip
{

    public float delayBeforeFiring = 0.5f;

    public AudioClip explosionAudioClip = null;
    public AudioClip hitAudioClip = null;
    private AudioSource audioSource = null;

    public SpriteRenderer mainRenderer;

    protected void Awake()
    {
        base.Awake();
        if (GetComponent<AutoShooter>()) {
            GetComponent<AutoShooter>().enabled = false;
            StartCoroutine(WaitBeforeFiring());
        }

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }


    public override void OnDamageTaken(int amount)
    {
        audioSource.PlayOneShot(hitAudioClip);
        health -= amount;
        StartCoroutine("colliderFlash");
        if (health <= 0)
        {
            OnDeath();
            Debug.Log("damageTaken");
            return;
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
        Debug.Log("death");
        audioSource.PlayOneShot(explosionAudioClip);
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
