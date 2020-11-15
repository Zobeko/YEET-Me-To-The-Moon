using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{

    public float fireRate;
    public int burstAmount;
    public float burstCooldown;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public GameObject[] shooterList;

    private float currentCooldown = 0;
    private bool unajusted = false;
    private bool inBurst = false;

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        } else if (!inBurst)
        {
            StartCoroutine(FireBurst());
        }
    }

    private void Shoot()
    {
        GameObject instantiated;

        //On fait le tour des shooters passes par l'inspecteur et on tire
        foreach (GameObject shooter in shooterList)
        {
            instantiated = Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
            instantiated.GetComponent<Rigidbody2D>().velocity = shooter.transform.up*bulletSpeed;
        }

    }

    public void Unajust()
    {
        unajusted = true;
    }

    IEnumerator FireBurst()
    {
        inBurst = true;
        for(int i = 0; i < burstAmount; i++)
        {
            Shoot();

            if (!unajusted)
            {
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                yield return new WaitForSeconds(fireRate + Random.Range(-0.2f, 0.2f));
            }
        }

        inBurst = false;
        currentCooldown = burstCooldown;
    }

}
