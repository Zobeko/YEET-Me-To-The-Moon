using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{

    public float fireRate;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public GameObject[] shooterList;

    private float currentCooldown = 0;
    private bool unajusted = false;

    private void Update()
    {

        if(currentCooldown <= 0)
        {
            Shoot();
        } else
        {
            currentCooldown -= Time.deltaTime;
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

        if (!unajusted)
        {
            currentCooldown = fireRate;
        } else
        {
            currentCooldown = fireRate + Random.Range(-0.2f, 0.2f);
        }

    }

    public void Unajust()
    {
        unajusted = true;
    }

}
