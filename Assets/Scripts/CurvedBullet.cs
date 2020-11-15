using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedBullet : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationSlowDown;
    public float bulletAcceleration;
    public float bulletSpeed;

    private Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rBody.rotation += rotationSpeed * Time.fixedDeltaTime;
        rotationSpeed = Mathf.Max(0, rotationSpeed - rotationSlowDown*Time.fixedDeltaTime);
        rBody.velocity = (Vector2)transform.up * bulletAcceleration;
    }
}
